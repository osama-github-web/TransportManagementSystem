﻿using IMS.Application.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Application.EFCore.Repositories;

public class VehicleMaintenanceRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleMaintenanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleMaintenance>?> GetAllVehiclesWithMaintenancesAsync()
    {
        return await (from vehicle in _context.Vehicles
                      join vehicleMaintenance in _context.VehicleMaintenances on vehicle.Id equals vehicleMaintenance.VehicleId
                      join maintenanceType in _context.MaintenanceTypes on vehicleMaintenance.MaintenanceTypeId equals maintenanceType.Id
                      select new VehicleMaintenance
                      {
                          Id = vehicleMaintenance.Id,
                          VehicleId = vehicle.Id,
                          Vehicle = vehicle,
                          MaintenanceType = maintenanceType,
                          MaintenanceTypeId = maintenanceType.Id
                      }).ToListAsync();
    }




    public async Task<VehicleMaintenance?> GetVehicleMaintenanceAsync(int vehicleId, int maintenanceId)
    {
        return await (from vehicle in _context.Vehicles
                      join vehicleMaintenance in _context.VehicleMaintenances on vehicle.Id equals vehicleMaintenance.VehicleId
                      join maintenanceType in _context.MaintenanceTypes on vehicleMaintenance.MaintenanceTypeId equals maintenanceType.Id
                      where vehicleMaintenance.VehicleId == vehicleId && vehicleMaintenance.MaintenanceTypeId == maintenanceId
                      select new VehicleMaintenance
                      {
                          VehicleId = vehicle.Id,
                          Vehicle = vehicle,
                          MaintenanceType = maintenanceType,
                          MaintenanceTypeId = maintenanceType.Id
                      }).FirstOrDefaultAsync();
    }

    public async Task<List<MaintenanceType>?> GetVehicleMaintenancesAsync(int vehicleId)
    {
        return await (from vehicle in _context.Vehicles
                      join vehicleMaintenance in _context.VehicleMaintenances on vehicle.Id equals vehicleMaintenance.VehicleId
                      join maintenanceType in _context.MaintenanceTypes on vehicleMaintenance.MaintenanceTypeId equals maintenanceType.Id
                      where vehicleMaintenance.VehicleId == vehicleId
                      select maintenanceType).ToListAsync();
    }

    public async Task<VehicleMaintenance?> RemoveVehicleMaintenanceAsync(VehicleMaintenance vehicleMaintenance)
    {
        var _vehicleMaintenance = await GetVehicleMaintenanceAsync(vehicleMaintenance.VehicleId, vehicleMaintenance.MaintenanceTypeId);
        if (_vehicleMaintenance is null)
            return null;

        _context.Remove<VehicleMaintenance>(_vehicleMaintenance);
        if (await _context.SaveChangesAsync() > 0)
            return _vehicleMaintenance;
        return null;
    }

    public async Task<VehicleMaintenance?> AddVehicleMaintenanceAsync(VehicleMaintenance vehicleMaintenance)
    {
        await _context.AddAsync<VehicleMaintenance>(vehicleMaintenance);
        if (await _context.SaveChangesAsync() > 0)
            return vehicleMaintenance;
        return null;
    }

    public async Task<VehicleMaintenance?> GetVehicleMaintenanceAsync(int id)
    {
        return await _context.VehicleMaintenances.FindAsync(id);
    }

    public async Task<VehicleMaintenance?> RemoveVehicleMaintenanceAsync(int id)
    {
        var vehicleMaintenance = await GetVehicleMaintenanceAsync(id);
        if (vehicleMaintenance is null)
            return null;

        _context.Remove<VehicleMaintenance>(vehicleMaintenance);
        if (await _context.SaveChangesAsync() > 0)
            return vehicleMaintenance;
        return null;
    }
}
