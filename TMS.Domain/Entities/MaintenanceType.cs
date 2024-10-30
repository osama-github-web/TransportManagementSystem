using System.ComponentModel.DataAnnotations;

namespace TMS.Domain.Entities;

public class MaintenanceType
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public MaintenanceType Update(MaintenanceType maintenanceType)
    {
        if (!string.IsNullOrWhiteSpace(maintenanceType.Name))
            this.Name = maintenanceType.Name;
        if (!string.IsNullOrWhiteSpace(maintenanceType.Description))
            this.Description = maintenanceType.Description;
        this.UpdatedDate = DateTime.Now;
        return this;
    }
}
