using IMS.Application.EFCore.Repositories;
using TMS.Application.EFCore.Repositories;
using TMS.Infrastructure.Services;

namespace TMS.Api.Extensions;

public static class RegisterServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<MaintenanceTypeRepository, MaintenanceTypeRepository>();
        services.AddScoped<RolesRepository, RolesRepository>();
        services.AddScoped<VehicleMaintenanceRepository, VehicleMaintenanceRepository>();
        services.AddScoped<VehicleRepository, VehicleRepository>();

        services.AddScoped<ApplicationUserService, ApplicationUserService>();
        services.AddScoped<VehicleService, VehicleService>();
        services.AddScoped<MaintenanceTypeService, MaintenanceTypeService>();
        services.AddScoped<VehicleMaintenanceService, VehicleMaintenanceService>();
        return services;
    }
}
