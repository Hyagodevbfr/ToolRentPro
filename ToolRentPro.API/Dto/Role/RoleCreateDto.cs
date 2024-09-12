using System.ComponentModel.DataAnnotations;

namespace ToolRentPro.API.Dto.Role;

public class RoleCreateDto
{
    [Required(ErrorMessage = "O nome da função é obrigatório.")]
    public string RoleName { get; set; } = string.Empty;
}
