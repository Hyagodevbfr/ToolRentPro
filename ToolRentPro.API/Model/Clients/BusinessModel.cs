using System.ComponentModel.DataAnnotations;

namespace ToolRentPro.API.Model.Clients;

public class BusinessModel : EntityPerson
{
    [Required]
    public string CompanyName { get; set; } = null!;
    [Required]
    public string TradeName { get; set; } = null!;
}
