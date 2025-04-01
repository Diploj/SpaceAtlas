using AutoMapper;
using SpaceAtlas.BL.Planet.Entities;
using SpaceAtlas.Controllers.Planet.Entities;

namespace SpaceAtlas.Mapper;

public class PlanetProfile : Profile
{
    public PlanetProfile()
    {
        CreateMap<PlanetCreateRequest, PlanetModel>();
        CreateMap<PlanetUpdateRequest, PlanetModel>();
        CreateMap<PlanetFilter, FilterPlanetModel>();
        CreateMap<PlanetModel, PlanetResponse>();
    }
}