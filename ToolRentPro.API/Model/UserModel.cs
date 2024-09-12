using Microsoft.AspNetCore.Identity;

namespace ToolRentPro.API.Model;

public class UserModel : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
