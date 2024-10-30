using TMS.Application.EFCore.Repositories;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Services;

public class VehicleService
{
    private readonly VehicleRepository _vehicleRepository;

    public VehicleService(VehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<List<Vehicle>?> GetAllVehiclesAsync()
    {
        return await _vehicleRepository.GetAllVehiclesAsync();
    }

    public async Task<Vehicle?> GetVehicleAsync(int? id = null, string? vin = null)
    {
        return await _vehicleRepository.GetVehicleAsync(id, vin);
    }

    public async Task<Vehicle?> AddVehicleAsync(Vehicle vehicle)
    {
        return await _vehicleRepository.AddVehicleAsync(vehicle);
    }

    public async Task<Vehicle?> UpdateVehicleAsync(Vehicle vehicle)
    {
        return await _vehicleRepository.UpdateVehicleAsync(vehicle);
    }

    public async Task<Vehicle?> RemoveVehicleAsync(Vehicle vehicle)
    {
        return await _vehicleRepository.RemoveVehicleAsync(vehicle);
    }
}
