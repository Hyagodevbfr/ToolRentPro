using AutoMapper;
using ToolRentPro.API.Dto.User;
using ToolRentPro.API.Model;

namespace ToolRentPro.API.Profiles;

public class ProfileAutoMapper : Profile
{
    public ProfileAutoMapper()
    {
        CreateMap<UserRegisterDto,UserModel>( );
    }
}
