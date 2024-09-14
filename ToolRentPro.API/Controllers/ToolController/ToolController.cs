using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToolRentPro.API.Dto.Tool;
using ToolRentPro.API.Infra;
using ToolRentPro.API.Model.Tool;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Controllers.ToolController;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ToolController: ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly UserManager<UserModel> _userManager;
    private readonly string _imagePath;

    public ToolController(AppDbContext appDbContext, IMapper mapper, UserManager<UserModel> userManager, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _userManager = userManager;
        _imagePath = Path.Combine(Directory.GetCurrentDirectory( ),"Images");

    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> CreateTool(IFormFile photo,[FromForm] ToolCreateDto toolCreateDto)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = Guid.Parse(currentUserId!);

        if(photo == null || photo.Length == 0)
        {
            return BadRequest("Nenhum arquivo enviado.");
        }

        var pathName = await GenerateNameImage(photo);

        var tool = _mapper.Map<ToolModel>(toolCreateDto,opts =>
        {
            opts.Items["UserId"] = user;
        });
        tool.Photo = pathName;
        
        await _appDbContext.AddAsync(tool);
        await _appDbContext.SaveChangesAsync();

        return Ok(new { Message = "Ferramenta criada com sucesso."});
    }

    private async Task<string> GenerateNameImage(IFormFile photo)
    {
        var uniqueCode = Guid.NewGuid( ).ToString("");
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(photo.FileName);
        var pathName = fileNameWithoutExtension.Replace(" ","").ToLower( ) + uniqueCode + ".png";
        var filePath = Path.Combine(_imagePath,pathName);

        if(!Directory.Exists(_imagePath))
            Directory.CreateDirectory(_imagePath);

        using(var stream = new FileStream(filePath,FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        return pathName;
    }
}
