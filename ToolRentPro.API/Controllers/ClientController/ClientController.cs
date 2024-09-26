using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToolRentPro.API.Infra;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Controllers.ClientController;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ClientController: ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly UserManager<UserModel> _userManager;
    public ClientController(AppDbContext appDbContext,IMapper mapper,UserManager<UserModel> userManager)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _userManager = userManager;
    }

}
