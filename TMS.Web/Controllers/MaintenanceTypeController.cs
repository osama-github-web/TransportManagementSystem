using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Entities;
using TMS.Infrastructure.Services;

namespace TMS.Web.Controllers
{
    //[Authorize(Roles = "Admin,User")]
    public class MaintenanceTypeController : Controller
    {
        private readonly MaintenanceTypeService _maintenanceTypeService;

        public MaintenanceTypeController(MaintenanceTypeService maintenanceTypeService)
        {
            _maintenanceTypeService = maintenanceTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _maintenanceTypeService.GetAllMaintenanceTypesAsync();
            return View(vehicles);
        }

        [HttpGet("MaintenanceType/GetMaintenanceTypeById/{maintenanceTypeId}")]
        public async Task<JsonResult> GetMaintenanceTypeById(int maintenanceTypeId)
        {
            var maintenanceType = await _maintenanceTypeService.GetMaintenanceTypeAsync(maintenanceTypeId);
            return Json(maintenanceType);
        }

        [HttpPost("MaintenanceType/Add")]
        public async Task<JsonResult> AddMaintenanceType(MaintenanceType maintenanceType)
        {
            var _maintenanceType = await _maintenanceTypeService.AddMaintenanceTypesAsync(maintenanceType);
            if (_maintenanceType is null)
                return Json(new { Message = "MaintenanceType Not Added", MaintenanceType = maintenanceType });
            return Json(new { Message = "MaintenanceType Added Successfully", MaintenanceType = _maintenanceType });
        }
        
        [HttpPost("MaintenanceType/Update")]
        public async Task<JsonResult> UpdateMaintenanceType(MaintenanceType maintenanceType)
        {
            var _maintenanceType = await _maintenanceTypeService.UpdateMaintenanceTypesAsync(maintenanceType);
            if (_maintenanceType is null)
                return Json(new { Message = "MaintenanceType Not Updated", MaintenanceType = maintenanceType });
            return Json(new { Message = "MaintenanceType Updated Successfully", MaintenanceType = _maintenanceType });
        }
        
        [HttpPost("MaintenanceType/Delete")]
        public async Task<JsonResult> DeleteMaintenanceType(MaintenanceType maintenanceType)
        {
            var _maintenanceType = await _maintenanceTypeService.RemoveMaintenanceTypesAsync(maintenanceType);
            if (_maintenanceType is null)
                return Json(new { Message = "MaintenanceType Not Added", MaintenanceType = maintenanceType });
            return Json(new { Message = "MaintenanceType Deleted Successfully", VeMaintenanceTypehicle = _maintenanceType });
        }
    }
}
