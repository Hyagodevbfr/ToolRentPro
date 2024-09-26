using System.ComponentModel.DataAnnotations;

namespace ToolRentPro.API.Model.Clients;

public class PhoneModel
{
    public Guid Id { get; set; } = Guid.NewGuid( );
    [Required]
    public string Number { get; set; } = string.Empty;

    public Guid ClientId { get; set; }
    public EntityPerson Client { get; set; } = null!;

}
