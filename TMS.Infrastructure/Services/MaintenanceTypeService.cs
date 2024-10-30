using TMS.Application.EFCore.Repositories;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Services;

public class MaintenanceTypeService
{
    private readonly MaintenanceTypeRepository _maintenanceTypeRepository;

    public MaintenanceTypeService(MaintenanceTypeRepository maintenanceTypeRepository)
    {
        _maintenanceTypeRepository = maintenanceTypeRepository;
    }

    public async Task<List<MaintenanceType>?> GetAllMaintenanceTypesAsync()
    {
        return await _maintenanceTypeRepository.GetAllMaintenanceTypesAsync();
    }

    public async Task<MaintenanceType?> GetMaintenanceTypeAsync(int id)
    {
        return await _maintenanceTypeRepository.GetMaintenanceTypeAsync(id);
    }

    public async Task<MaintenanceType?> AddMaintenanceTypesAsync(MaintenanceType maintenanceType)
    {
        return await _maintenanceTypeRepository.AddMaintenanceTypeAsync(maintenanceType);
    }

    public async Task<MaintenanceType?> UpdateMaintenanceTypesAsync(MaintenanceType maintenanceType)
    {
        return await _maintenanceTypeRepository.UpdateMaintenanceTypesAsync(maintenanceType);
    }

    public async Task<MaintenanceType?> RemoveMaintenanceTypesAsync(MaintenanceType maintenanceType)
    {
        return await _maintenanceTypeRepository.RemoveMaintenanceTypesAsync(maintenanceType);
    }
}
