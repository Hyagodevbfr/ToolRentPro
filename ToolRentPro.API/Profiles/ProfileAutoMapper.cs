using AutoMapper;
using ToolRentPro.API.Dto.Category;
using ToolRentPro.API.Dto.Tool;
using ToolRentPro.API.Dto.User;
using ToolRentPro.API.Model.Categories;
using ToolRentPro.API.Model.Tool;
using ToolRentPro.API.Model.User;

namespace ToolRentPro.API.Profiles;

public class ProfileAutoMapper : Profile
{
    public ProfileAutoMapper()
    {
        CreateMap<UserRegisterDto,UserModel>( );

        CreateMap<CategoryDto,Category>( )
            .ForMember(dest => dest.CreatedBy,opt => opt.MapFrom((src,dest,_,context) => context.Items["UserId"]))
            .ForMember(dest => dest.CreatedAt,opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.EditedBy,opt => opt.MapFrom((src,dest,_,context) => context.Items["UserId"]))
            .ForMember(dest => dest.LastUpdate,opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<ToolCreateDto,ToolModel>()
            .ForMember(dest => dest.CreatedBy,opt => opt.MapFrom((src,dest,_,context) => context.Items["UserId"]))
            .ForMember(dest => dest.CreatedAt,opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.EditedBy,opt => opt.MapFrom((src,dest,_,context) => context.Items["UserId"]))
            .ForMember(dest => dest.LastUpdate,opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}
