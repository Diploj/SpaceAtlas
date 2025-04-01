using System.Data;
using AutoMapper;
using SpaceAtlas.BL.Planet.Entities;
using SpaceAtlas.DataAccess.Entities;
using SpaceAtlas.DataAccess.Repository;

namespace SpaceAtlas.BL.Planet;

public class PlanetService : IPlanetService
{
    
    private readonly IRepository<PlanetEntity> _planetRepository;
    private readonly IMapper _mapper;

    public PlanetService(IRepository<PlanetEntity> planetRepository, IMapper mapper)
    {
        _planetRepository = planetRepository;
        _mapper = mapper;
    }
    public IEnumerable<PlanetModel> GetAll(FilterPlanetModel? filter = null)
    {
        var planets = _planetRepository.GetAll(x =>
            filter == null ||
            (filter.Name == null || x.Name == filter.Name) ||
            (filter.UserId == null || x.UserId == filter.UserId) ||
            (filter.StarId == null || x.StarId == filter.StarId) ||
            (filter.HasAtmosphere == null || x.HasAtmosphere == filter.HasAtmosphere));
        return _mapper.Map<IEnumerable<PlanetModel>>(planets);
    }

    public PlanetModel GetById(Guid planetId)
    {
        var planet = _planetRepository.GetById(planetId);
        if(planet == null)
            throw new KeyNotFoundException();
        return _mapper.Map<PlanetModel>(planet);
    }

    public Guid Create(PlanetModel planet)
    {
        planet.Id = Guid.NewGuid();
        var planetEntity = _mapper.Map<PlanetEntity>(planet);
        return _planetRepository.Save(planetEntity);
    }

    public Guid Update(PlanetModel planet)
    {
        if (planet.Id != null)
        {
            var planetEntity = _planetRepository.GetById(planet.Id.Value);
            if (planetEntity == null)
                throw new KeyNotFoundException();
            planetEntity = _mapper.Map<PlanetEntity>(planet);
            return _planetRepository.Save(planetEntity);
        }
        else
        {
            throw new NoNullAllowedException();
        }
    }

    public void Delete(Guid planetId)
    {
        _planetRepository.Delete(planetId);
    }
}