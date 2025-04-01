using AutoMapper;
using SpaceAtlas.BL.Star.Entities;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.BL.Mapper;

public class StarBLProfile : Profile
{
    public StarBLProfile()
    {
        CreateMap<StarModel, StarEntity>();
    }
}