using IMS.Application.EFCore.Data;
using IMS.Application.EFCore.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TMS.Api.Extensions;
using TMS.Domain.Entities;
using TMS.Domain.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly("TMS.Application"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Home/Login"; // Set the login path here
});

builder.Services.ConfigureApplicationCookie(options =>
{
    //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "HotelAuthentication";
    options.Cookie.HttpOnly = true;
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(1000);
    options.LoginPath = "/Home/Login";
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    //options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    //options.SlidingExpiration = true;
});

builder.Services.AddServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


await AddDefaultUsers(app);
app.Run();


async Task AddDefaultUsers(WebApplication app)
{
    var users = new List<ApplicationUser>
    {
        new()
        {
            UserName = "admin",
            Email = "admin@admin.com",
            PhoneNumber = "02220514774",
            Password = "admin",
            Role = ERoles.Admin.ToString()
        },
        new()
        {
            UserName = "user",
            Email = "user@admin.com",
            PhoneNumber = "02220514774",
            Password = "user",
            Role = ERoles.User.ToString()
        }
    };

    using (var scope = app.Services.CreateAsyncScope())
    {
        var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
        var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
        foreach (var user in users)
        {
            var _user = await userManager.FindByEmailAsync(user.Email);
            if (_user is null)
            {
                await userManager.CreateAsync(user,user.Password);
                if (await roleManager.RoleExistsAsync(user.Role))
                    await roleManager.CreateAsync(new IdentityRole(user.Role));

                await userManager.AddToRoleAsync(user,user.Role);
            }
        }
    }
}