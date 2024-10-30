using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;
using TMS.Domain.Enums;

namespace IMS.Application.EFCore.Repositories;

public class RolesRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<IdentityRole>?> GetAllRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<Task> AddDefaultRolesAsync(ApplicationUser applicationUser)
    {
        foreach (string role in Enum.GetNames(typeof(ERoles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }
        return Task.CompletedTask;
    }

    public async Task<IdentityRole?> GetRoleAsync(string role)
    {
        return await _roleManager.FindByNameAsync(role);
    }

    public async Task<IdentityRole?> AddRoleAsync(string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }
        return await GetRoleAsync(role);
    }
}
