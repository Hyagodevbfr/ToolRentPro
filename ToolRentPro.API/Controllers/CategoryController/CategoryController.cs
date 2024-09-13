using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToolRentPro.API.Dto.Category;
using ToolRentPro.API.Infra;
using ToolRentPro.API.Model.Categories;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Controllers.CategoryController;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoryController: ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CategoryController(UserManager<UserModel> userManager, AppDbContext appDbContext, IMapper mapper)
    {
        _userManager = userManager;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] CategoryDto createDto)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = Guid.Parse(currentUserId!);

        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var category = _mapper.Map<Category>(createDto, opts =>
        {
            opts.Items["UserId"] = user;
        });

        var categoryExist = await _appDbContext.Categories!.FirstOrDefaultAsync(c => c.Name == createDto.Name);
        if(categoryExist != null)
            return Conflict("Já existe essa categoria.");

        _appDbContext.Categories!.Add(category);
        await _appDbContext.SaveChangesAsync();
        return Ok(category);
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<CategoryDto>>> GetCategories()
    {
        var categories = await _appDbContext.Categories!.ToListAsync( );
        var categoryDtos = new List<CategoryDto>();

        foreach (var category in categories)
        {
            var categoryDto = new CategoryDto
            {
                Name = category.Name,
            };
            categoryDtos.Add(categoryDto);
        }
        return Ok(categoryDtos);
    }

    [HttpPatch("/category/{id:guid}")]
    public async Task<ActionResult<Category>> PutCategory([FromRoute]Guid id,[FromBody]CategoryDto categoryDto)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = Guid.Parse(currentUserId!);

        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var category = await _appDbContext.Categories!.FirstOrDefaultAsync(c => c.Id == id);
        if(category is null)
            return NotFound("Categoria não localizada.");

        category.Name = categoryDto.Name;
        category.EditedBy = user;
        category.LastUpdate = DateTime.UtcNow;

        if(string.IsNullOrEmpty(category.Name))
            return BadRequest("Nome para categoria é obrigatório.");

        _appDbContext.Categories!.Update(category);
        await _appDbContext.SaveChangesAsync( );
        return Ok("Categoria editada com sucesso.");

    }

}
