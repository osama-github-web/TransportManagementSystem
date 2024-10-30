using TMS.Application.EFCore.Repositories;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Services;

public class VehicleMaintenanceService
{
    private readonly VehicleRepository _vehicleRepository;
    private readonly MaintenanceTypeRepository _maintenanceTypeRepository;
    private readonly VehicleMaintenanceRepository _vehicleMaintenanceRepository;

    public VehicleMaintenanceService(VehicleRepository vehicleRepository, MaintenanceTypeRepository maintenanceTypeRepository, VehicleMaintenanceRepository vehicleMaintenanceRepository)
    {
        _vehicleRepository = vehicleRepository;
        _maintenanceTypeRepository = maintenanceTypeRepository;
        _vehicleMaintenanceRepository = vehicleMaintenanceRepository;
    }

    
    public async Task<VehicleMaintenance?> AddVehicleMaintenanceAsync(VehicleMaintenance vehicleMaintenance)
    {
        vehicleMaintenance.Vehicle = await _vehicleRepository.GetVehicleAsync(vehicleMaintenance.VehicleId);
        vehicleMaintenance.MaintenanceType = await _maintenanceTypeRepository.GetMaintenanceTypeAsync(vehicleMaintenance.MaintenanceTypeId);
        return await _vehicleMaintenanceRepository.AddVehicleMaintenanceAsync(vehicleMaintenance);
    }
    
    public async Task<List<VehicleMaintenance>?> GetAllVehiclesWithMaintenancesAsync()
    {
        return await _vehicleMaintenanceRepository.GetAllVehiclesWithMaintenancesAsync();
    }
    
    public async Task<Vehicle?> GetVehicleMaintenancesAsync(int vehicleId)
    {
        var vehicle = await _vehicleRepository.GetVehicleAsync(vehicleId);
        if(vehicle is not null)
        vehicle.MaintenanceTypes = await _vehicleMaintenanceRepository.GetVehicleMaintenancesAsync(vehicleId);
        return vehicle;
    }
    
    public async Task<VehicleMaintenance?> RemoveVehicleMaintenanceAsync(int vehicleId)
    {
        return await _vehicleMaintenanceRepository.RemoveVehicleMaintenanceAsync(vehicleId);
    }
}
