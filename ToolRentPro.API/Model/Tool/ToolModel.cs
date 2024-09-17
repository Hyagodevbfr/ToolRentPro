using System.ComponentModel.DataAnnotations;
using ToolRentPro.API.Enums.Tool;
using ToolRentPro.API.Model.Categories;
using ToolRentPro.API.Model.Maintenances;
using ToolRentPro.API.Model.RentalTools;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Model.Tool;

public class ToolModel : Entity
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    [Required]
    public string NameTool { get; set; } = null!;
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
    public ICollection<RentalTool>? RentalTool { get; set; } = new List<RentalTool>( );
    public bool NecessaryMaintenance { get; set; }
    public ICollection<Maintenance>? MaintenanceHistory { get; set; } = new List<Maintenance>( );
}
