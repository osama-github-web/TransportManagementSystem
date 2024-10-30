using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Domain.Entities;
using TMS.Infrastructure.Services;

namespace TMS.Web.Controllers;

//[Authorize(Roles = "Admin,User")]
public class VehicleController : Controller
{
    private readonly VehicleService _vehicleService;

    public VehicleController(VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<IActionResult> Index()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        return View(vehicles);
    }

    [HttpGet("Vehicle/GetVehicleById/{vehicleId}")]
    public async Task<JsonResult> GetVehicleById(int vehicleId)
    {
        var vehicle = await _vehicleService.GetVehicleAsync(vehicleId);
        return Json(vehicle);
    }

    [HttpPost("Vehicle/Add")]
    public async Task<JsonResult> AddVehicle(Vehicle vehicle)
    {
        var _vehicle = await _vehicleService.AddVehicleAsync(vehicle);
        if (_vehicle is null)
            return Json(new { Message = "Vehicle Not Added", Vehicle = vehicle });
        return Json(new { Message = "Vehicle Added Successfully", Vehicle = _vehicle });
    }
    
    [HttpPost("Vehicle/Update")]
    public async Task<JsonResult> UpdateVehicle(Vehicle vehicle)
    {
        var _vehicle = await _vehicleService.UpdateVehicleAsync(vehicle);
        if (_vehicle is null)
            return Json(new { Message = "Vehicle Not Updated", Vehicle = vehicle });
        return Json(new { Message = "Vehicle Updated Successfully", Vehicle = _vehicle });
    }
    
    [HttpPost("Vehicle/Delete")]
    public async Task<JsonResult> DeleteVehicle(Vehicle vehicle)
    {
        var _vehicle = await _vehicleService.RemoveVehicleAsync(vehicle);
        if (_vehicle is null)
            return Json(new { Message = "Vehicle Not Added", Vehicle = vehicle });
        return Json(new { Message = "Vehicle Deleted Successfully", Vehicle = _vehicle });
    }
}
