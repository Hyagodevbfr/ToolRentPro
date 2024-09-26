using System.ComponentModel.DataAnnotations;

namespace ToolRentPro.API.Model.Clients;

public class IndividualModel : EntityPerson
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string CPF { get; set; } = null!;
    [Required]
    public string Rg { get; set; } = null!;
}
