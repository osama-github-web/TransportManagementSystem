using IMS.Application.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Application.EFCore.Repositories;

public class VehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vehicle>?> GetAllVehiclesAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleAsync(int? id = null, string? vin = null)
    {
        if(id.HasValue)
            return await _context.Vehicles.FindAsync(id);
        if (!string.IsNullOrWhiteSpace(vin))
            return await _context.Vehicles.FirstOrDefaultAsync(x=> x.VIN.Equals(vin));
        return null;
    }
    
    public async Task<Vehicle?> AddVehicleAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
        if (await _context.SaveChangesAsync() > 0)
            return vehicle;
        return null;
    }
    
    public async Task<Vehicle?> UpdateVehicleAsync(Vehicle vehicle)
    {
        var _vehicle = await GetVehicleAsync(id:vehicle.Id);
        if (_vehicle is null)
            return null;

        _vehicle.Update(vehicle);

        _context.Vehicles.Update(_vehicle);
        if (await _context.SaveChangesAsync() > 0)
            return _vehicle;
        return null;
    }
    
    public async Task<Vehicle?> RemoveVehicleAsync(Vehicle vehicle)
    {
        var _vehicle = await GetVehicleAsync(id:vehicle.Id);
        if (_vehicle is null)
            return null;

        _context.Remove<Vehicle>(_vehicle);
        if (await _context.SaveChangesAsync() > 0)
            return _vehicle;
        return null;
    }
}
