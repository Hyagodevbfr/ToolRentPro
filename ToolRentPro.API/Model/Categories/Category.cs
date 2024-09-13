using System.ComponentModel.DataAnnotations;

namespace ToolRentPro.API.Model.Categories;

public class Category : Entity
{
    [Required]
    public string Name { get; set; } = null!;
}
