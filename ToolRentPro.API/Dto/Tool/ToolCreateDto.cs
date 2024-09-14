using System.ComponentModel.DataAnnotations;
using ToolRentPro.API.Enums.Tool;

namespace ToolRentPro.API.Dto.Tool;

public class ToolCreateDto
{
    public Guid CategoryId { get; set; }
    public string NameTool { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public decimal ToolCost { get; set; }
    public AvailabilityEnum Availability { get; set; }
}
