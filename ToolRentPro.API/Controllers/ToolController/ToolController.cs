using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using ToolRentPro.API.Dto.Role;
using ToolRentPro.API.Dto.Tool;
using ToolRentPro.API.Infra;
using ToolRentPro.API.Model.Categories;
using ToolRentPro.API.Model.Tool;
using ToolRentPro.API.Model.User;
using System.IO;


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

    [HttpPost("/createtool")]
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

    [HttpGet("/tools")]
    public async Task<ActionResult<IList<ToolResponseDto>>> GetTools(int page, int rows)
    {
        var tools = await _appDbContext.Tools!
        .Select(t => new ToolResponseDto
        {
            NameTool = t.NameTool,
            CategoryId = t.CategoryId,
            Description = t.Description,
            Photo = t.Photo,
            Brand = t.Brand,
            StockQuantity = t.StockQuantity,
            ToolCost = t.ToolCost,
            Availability = t.Availability,
            NecessaryMaintenance = t.NecessaryMaintenance
        }).Skip(page - 1 * rows).Take(rows)
        .ToListAsync( );

        return Ok(tools);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ToolModel>> GetDetail(Guid id)
    {
        var tool = await _appDbContext.Tools!.FindAsync(id);
        if(tool is null)
            return NotFound("Ferramenta não encontrada ou apagada.");

        return Ok(tool);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> UpdateTool(IFormFile photo, Guid id,[FromForm] ToolUpdateDto toolUpdateDto)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = Guid.Parse(currentUserId!);

        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if(photo is null)
            return BadRequest("Insira uma foto.");

        var tool =  await _appDbContext.Tools!.FirstOrDefaultAsync(t => t.Id == id);
        if(tool is null)
            return NotFound("Ferramenta não encontrada ou apagada.");

        var pathName = "";
        if(photo is not null)
        {
            string existingPath = _imagePath + tool.Photo;
            if(System.IO.File.Exists(existingPath))
                System.IO.File.Delete(existingPath);

            pathName = await GenerateNameImage(photo!);
        }

        tool.NameTool = toolUpdateDto.NameTool;
        tool.Description = toolUpdateDto.Description;
        tool.CategoryId = toolUpdateDto.CategoryId;
        tool.Brand = toolUpdateDto.Brand;
        tool.StockQuantity = toolUpdateDto.StockQuantity;
        tool.ToolCost = toolUpdateDto.ToolCost;
        tool.Availability = toolUpdateDto.Availability;
        tool.NecessaryMaintenance = toolUpdateDto.NecessaryMaintenance;

            
        if(photo != null)
            tool.Photo = pathName;
        else 
            tool.Photo = tool.Photo;

        tool.EditedBy = user;
        tool.LastUpdate = DateTime.UtcNow;

        _appDbContext.Update(tool);
        await _appDbContext.SaveChangesAsync();
        return Ok(tool);

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
