using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TMS.Domain.Entities;

namespace IMS.Application.EFCore.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, IConfiguration _configuration)
    : base(dbContextOptions) { }

    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<MaintenanceType> MaintenanceTypes { get; set; }
    public DbSet<VehicleMaintenance> VehicleMaintenances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //optionsBuilder.UseSqlServer(
        //    _configuration.GetConnectionString("DefaultConnection"),
        //    x => x.MigrationsAssembly("IMS.Application.EFCore.Data.Migrations")
        //    );
    }
}