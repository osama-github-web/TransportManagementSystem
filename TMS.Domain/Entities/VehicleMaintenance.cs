using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Domain.Entities;

public class VehicleMaintenance
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int MaintenanceTypeId { get; set; }

    [NotMapped]
    public Vehicle? Vehicle { get; set; }
    [NotMapped]
    public MaintenanceType? MaintenanceType { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public VehicleMaintenance Update(VehicleMaintenance vehicleMaintenance)
    {
        if (vehicleMaintenance.VehicleId > 0)
            this.VehicleId = vehicleMaintenance.VehicleId;
        if (vehicleMaintenance.MaintenanceTypeId > 0)
            this.MaintenanceTypeId = vehicleMaintenance.MaintenanceTypeId;
        this.UpdatedDate = DateTime.Now;
        return this;
    }
}
