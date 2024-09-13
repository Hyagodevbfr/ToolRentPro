using System.ComponentModel.DataAnnotations;

namespace ToolRentPro.API.Model;

public abstract class Entity
{
    [Key] public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid EditedBy { get; set; }
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

    protected Entity()
    {
        Id = Guid.NewGuid( );
    }
}
