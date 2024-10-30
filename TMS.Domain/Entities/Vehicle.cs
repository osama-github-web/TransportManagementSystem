using System.ComponentModel.DataAnnotations;

namespace TMS.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }
    [Required]
    public string VIN { get; set; }
    [Required]
    public string Number { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Vehicle Update(Vehicle vehicle)
    {
        if(!string.IsNullOrWhiteSpace(vehicle.VIN))
            this.VIN = vehicle.VIN;
        if (!string.IsNullOrWhiteSpace(vehicle.Number))
            this.Number = vehicle.Number;
        if (!string.IsNullOrWhiteSpace(vehicle.Description))
            this.Description = vehicle.Description;
        if (!string.IsNullOrWhiteSpace(vehicle.Notes))
            this.Notes = vehicle.Notes;
        this.UpdatedDate = DateTime.Now;
        return this;
    }
}
