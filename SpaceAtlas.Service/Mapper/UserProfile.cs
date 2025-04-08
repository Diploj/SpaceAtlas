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
                opt => 
                    opt.MapFrom(src => MyHasher.Hash(src.Password)));
        CreateMap<UserUpdateRequest, UserModel>()
            .ForMember(dest => dest.PasswordHash,
                opt => 
                    opt.MapFrom(src => MyHasher.Hash(src.Password)));;
        CreateMap<UserFilter, FilterUserModel>();
        CreateMap<UserModel, UserResponse>();
    }
}