using AutoMapper;
using SpaceAtlas.BL.Planet.Entities;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.BL.Mapper;

public class PlanetBLProfile : Profile
{
    public PlanetBLProfile()
    {
        CreateMap<PlanetModel, PlanetEntity>();
        CreateMap<PlanetEntity, PlanetModel>();
    }
}