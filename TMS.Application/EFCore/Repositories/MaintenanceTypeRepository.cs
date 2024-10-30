using IMS.Application.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Application.EFCore.Repositories;

public class MaintenanceTypeRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MaintenanceType>?> GetAllMaintenanceTypesAsync()
    {
        return await _context.MaintenanceTypes.ToListAsync();
    }

    public async Task<MaintenanceType?> GetMaintenanceTypeAsync(int id)
    {
        return await _context.MaintenanceTypes.FindAsync(id);
    }

    public async Task<MaintenanceType?> AddMaintenanceTypeAsync(MaintenanceType maintenanceType)
    {
        await _context.MaintenanceTypes.AddAsync(maintenanceType);
        if (await _context.SaveChangesAsync() > 0)
            return maintenanceType;
        return null;
    }

    public async Task<MaintenanceType?> UpdateMaintenanceTypesAsync(MaintenanceType maintenanceType)
    {
        var _maintenanceType = await GetMaintenanceTypeAsync(maintenanceType.Id);
        if (_maintenanceType is null)
            return null;

        _maintenanceType.Update(maintenanceType);

        _context.MaintenanceTypes.Update(_maintenanceType);
        if (await _context.SaveChangesAsync() > 0)
            return _maintenanceType;
        return null;
    }

    public async Task<MaintenanceType?> RemoveMaintenanceTypesAsync(MaintenanceType maintenanceType)
    {
        var _maintenanceType = await GetMaintenanceTypeAsync(maintenanceType.Id);
        if (_maintenanceType is null)
            return null;

        _context.MaintenanceTypes.Remove(_maintenanceType);
        if (await _context.SaveChangesAsync() > 0)
            return _maintenanceType;
        return null;
    }
}
