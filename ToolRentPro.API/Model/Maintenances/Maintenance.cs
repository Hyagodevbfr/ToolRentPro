using System.ComponentModel.DataAnnotations;
using ToolRentPro.API.Enums.Maintenance;
using ToolRentPro.API.Model.Tool;

namespace ToolRentPro.API.Model.Maintenances;

public class Maintenance
{
    [Key]
    public Guid Id { get; set; }
    public Guid? ToolId { get; set; }
    public ToolModel? Tool { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? ProblemDescription { get; set; }
    public decimal MaintenanceCost { get; set; }
    public MaintenanceEnum Status { get; set; }


    protected Maintenance()
    {
        Id = Guid.NewGuid();
    }
}
