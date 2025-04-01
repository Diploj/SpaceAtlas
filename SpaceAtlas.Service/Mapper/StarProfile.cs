using AutoMapper;
using SpaceAtlas.BL.Star.Entities;
using SpaceAtlas.Controllers.Star.Entities;

namespace SpaceAtlas.Mapper;

public class StarProfile : Profile
{
    public StarProfile()
    {
        CreateMap<StarCreateRequest, StarModel>();
        CreateMap<StarUpdateRequest, StarModel>();
        CreateMap<StarFilter, FilterStarModel>();
        CreateMap<StarModel, StarResponse>();
    }
}