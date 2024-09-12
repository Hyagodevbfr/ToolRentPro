using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using ToolRentPro.API.Dto.Role;
using ToolRentPro.API.Model;

namespace ToolRentPro.API.Controllers.RoleController;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RoleController: ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<UserModel> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost("/create")]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto roleCreateDto)
    {
        if(roleCreateDto.RoleName is null)
            return BadRequest("O nome da função é obrigatório");

        var roleExist = await _roleManager.RoleExistsAsync(roleCreateDto.RoleName);
        if(roleExist)
            return BadRequest("Função já criada.");

        var result = await _roleManager.CreateAsync(new IdentityRole { Name = roleCreateDto.RoleName });
        if(result.Succeeded)
            return Ok(new {message = "Função criada com sucesso."});

        return BadRequest("Falha ao criar nova função.");
    }

    [HttpGet("/roles")]
    public async Task<ActionResult<IEnumerable<RoleResponseDto>>> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var roleDtos = new List<RoleResponseDto>();

        foreach(var role in roles)
        {
            var userInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            var roleDto = new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name,
                TotalUsers = userInRole.Count
            };
            roleDtos.Add(roleDto);
        }
        return Ok(roleDtos);

    }
    [HttpDelete("/delete/{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if(role is null)
            return NotFound("Função não encontrada ou já deletada.");

        var result = await _roleManager.DeleteAsync(role);
        if(result.Succeeded)
            return Ok(new { message = "Função deletada com sucesso." });

        return BadRequest("Falha ao tentar excluir função.");
    }

    [HttpPut("/assign")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleDto assignRoleDto)
    {
        var user = await _userManager.FindByIdAsync(assignRoleDto.UserId);
        if(user is null)
            return NotFound("Usuário não registrado.");

        var role = await _roleManager.FindByIdAsync(assignRoleDto.RoleId);
        if(role is null)
            return NotFound("Role não encontrada ou deletada.");

        var userRoles = await _userManager.GetRolesAsync(user);
        if(userRoles.Any() && !userRoles.Contains(role.Name!))
        {
            var removeRoles = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if(!removeRoles.Succeeded)
                return BadRequest("Falha ao remover role existente.");
        }

        var result = await _userManager.AddToRoleAsync(user,role.Name!);
        if(result.Succeeded)
            return Ok(new { message = "Função atribuída com sucesso." });

        var error = result.Errors.FirstOrDefault();
        return BadRequest(error!.Description);

    }
}
