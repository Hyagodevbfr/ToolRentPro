using System.ComponentModel.DataAnnotations;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Model.Tool;

public class ToolModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid( );
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
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
    public string Availability { get; set; } = string.Empty;
    public bool NecessaryMaintenance { get; set; }
    public List<string>? MaintenanceHistory { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedByUserId { get; set; }
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    public Guid LastUpdatedByUserId { get; set; }
}
