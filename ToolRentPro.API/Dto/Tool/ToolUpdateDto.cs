using ToolRentPro.API.Enums.Tool;

namespace ToolRentPro.API.Dto.Tool;

public class ToolUpdateDto
{
    public string NameTool { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public decimal ToolCost { get; set; }
    public AvailabilityEnum Availability { get; set; }
    public bool NecessaryMaintenance { get; set; }
}
