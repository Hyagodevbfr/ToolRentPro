using System.ComponentModel.DataAnnotations;
using ToolRentPro.API.Enums.Tool;
using ToolRentPro.API.Model.Categories;
using ToolRentPro.API.Model.Maintenances;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Model.Tool;

public class ToolModel : Entity
{
    public int CategoryId { get; set; }
    public Category? Category { get; private set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = string.Empty ;
    [Required]
    public string Photo { get; set; } = string.Empty;
    [Required]
    public string Brand { get; set; } = string.Empty;
    [Required]
    public int StockQuantity { get; set; }
    [Required]
    public decimal ToolCost { get; set; }
    [Required]
    public AvailabilityEnum Availability { get; set; }
    public bool NecessaryMaintenance { get; set; }
    public List<Maintenance>? MaintenanceHistoryId { get; set; }
    public List<Maintenance>? MaintenanceHistory { get; set; }
}
