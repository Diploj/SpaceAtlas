using AutoMapper;
using SpaceAtlas.BL.User.Entities;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.BL.Mapper;

public class UserBLProfile : Profile
{
    public UserBLProfile()
    {
        CreateMap<UserModel, UserEntity>();
        CreateMap<UserEntity,UserModel>();
    }
}