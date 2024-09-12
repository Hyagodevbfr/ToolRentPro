using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToolRentPro.API.Dto.Auth;
using ToolRentPro.API.Dto.User;
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
    private readonly IMapper _mapper;

    public AccountController(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("/register")]
    public async Task<ActionResult<string>> Register(UserRegisterDto userRegisterDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new UserModel {
            UserName = userRegisterDto.Email,
            Email = userRegisterDto.Email,
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
        };

        var result = await _userManager.CreateAsync(user,userRegisterDto.Password);

        if(!result.Succeeded)
            return BadRequest(result.Errors);

        if(userRegisterDto.Roles is null)
            await _userManager.AddToRoleAsync(user,"User");
        else
            foreach (var role in userRegisterDto.Roles)
            {
                await _userManager.AddToRoleAsync(user,role);
            }

        return Ok(new AuthResponseDto
        {
            IsSuccess = true,
            Message = "Conta criada com sucesso!"
        });
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
