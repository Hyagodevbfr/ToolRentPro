using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToolRentPro.API.Model;

namespace ToolRentPro.API.Controllers.AccountController;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    private async Task<string> GenerateToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler( );
        
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSetting").GetSection("securityKey").Value!);

        var roles =  await _userManager.GetRolesAsync(user);

        List<Claim> claims = [
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}" ?? ""),
            new(JwtRegisteredClaimNames.NameId, user.Id ?? ""),
            new(JwtRegisteredClaimNames.Aud, _configuration.GetSection("JwtSetting").GetSection("ValidAudience").Value! ?? ""),
            new(JwtRegisteredClaimNames.Iss, _configuration.GetSection("JwtSetting").GetSection("ValidIssuer").Value! ?? "")
        ];

        foreach(var role in roles) 
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
                )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
