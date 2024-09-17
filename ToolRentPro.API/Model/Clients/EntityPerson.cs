namespace ToolRentPro.API.Model.Clients;

public class EntityPerson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Guid ClienteId { get; set; }

    public string CEP { get; set; } = string.Empty;
    public string Addres { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;


    protected EntityPerson()
    {
        Id = Guid.NewGuid();
    }
}
