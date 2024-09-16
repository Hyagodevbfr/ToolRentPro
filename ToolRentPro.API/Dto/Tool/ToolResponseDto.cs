using System.ComponentModel.DataAnnotations;
using ToolRentPro.API.Enums.Tool;
using ToolRentPro.API.Model.Categories;

namespace ToolRentPro.API.Dto.Tool
{
    public class ToolResponseDto
    {
        public string NameTool { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal ToolCost { get; set; }
        public AvailabilityEnum Availability { get; set; }
        public bool NecessaryMaintenance { get; set; }
    }
}
