using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Entities;
using TMS.Infrastructure.Services;

namespace TMS.Web.Controllers;

[Authorize(Roles = "Admin,User")]
public class VehicleMaintenanceController : Controller
{
    private readonly MaintenanceTypeService _maintenanceTypeService;
    private readonly VehicleMaintenanceService _vehicleMaintenanceService;

    public VehicleMaintenanceController(MaintenanceTypeService maintenanceTypeService, VehicleMaintenanceService vehicleMaintenanceService)
    {
        _maintenanceTypeService = maintenanceTypeService;
        _vehicleMaintenanceService = vehicleMaintenanceService;
    }

    public async Task<IActionResult> Index()
    {
        var vehicleMaintenances = await _vehicleMaintenanceService.GetAllVehiclesWithMaintenancesAsync();
        return View(vehicleMaintenances);
    }

    [HttpPost("VehicleMaintenance/Delete")]
    public async Task<JsonResult> DeleteVehicle(VehicleMaintenance vehicleMaintenance)
    {
        var _vehicleMaintenance = await _vehicleMaintenanceService.RemoveVehicleMaintenanceAsync(vehicleMaintenance.Id);
        if (_vehicleMaintenance is null)
            return Json(new { Message = "VehicleMaintenance Not Deleted", VehicleMaintenance = _vehicleMaintenance });
        return Json(new { Message = "VehicleMaintenance Deleted Successfully", VehicleMaintenance = _vehicleMaintenance });
    }
}
