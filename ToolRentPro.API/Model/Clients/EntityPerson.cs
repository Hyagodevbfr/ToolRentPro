using System.ComponentModel.DataAnnotations;
using ToolRentPro.API.Enums.Client;

namespace ToolRentPro.API.Model.Clients;

public class EntityPerson : Entity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public ICollection<PhoneModel> Phones { get; set; } = new List<PhoneModel>();
    [Required] 
    public string CEP { get; set; } = string.Empty;
    [Required] 
    public string Addres { get; set; } = string.Empty;
    [Required] 
    public string Complement { get; set; } = string.Empty;
    [Required] 
    public string Neighborhood { get; set; } = string.Empty;
    [Required] 
    public string Location { get; set; } = string.Empty;
    [Required] 
    public string State { get; set; } = string.Empty;
    [Required] 
    public FidelityEnum Fidelity { get; set; }
}
