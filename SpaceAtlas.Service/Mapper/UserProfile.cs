using AutoMapper;
using SpaceAtlas.BL.User.Entities;
using SpaceAtlas.Controllers.User.Entities;
using SpaceAtlas.Algoritms;

namespace SpaceAtlas.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateRequest, UserModel>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore());
        CreateMap<UserUpdateRequest, UserModel>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore());
        CreateMap<UserFilter, FilterUserModel>();
        CreateMap<UserModel, UserResponse>();
    }
}