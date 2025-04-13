using System.Data;
using AutoMapper;
using SpaceAtlas.BL.Exceptions;
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
    public async Task<IEnumerable<PlanetModel>> GetAll(FilterPlanetModel? filter = null)
    {
        var planets = await _planetRepository.GetAll(x =>
            filter == null ||
            (filter.Name == null || x.Name == filter.Name) ||
            (filter.UserId == null || x.UserId == filter.UserId) ||
            (filter.StarId == null || x.StarId == filter.StarId) ||
            (filter.HasAtmosphere == null || x.HasAtmosphere == filter.HasAtmosphere));
        return _mapper.Map<IEnumerable<PlanetModel>>(planets);
    }

    public async Task<PlanetModel> GetById(Guid planetId)
    {
        var planet = await _planetRepository.GetById(planetId);
        if(planet == null)
            throw new KeyNotFoundException();
        return _mapper.Map<PlanetModel>(planet);
    }

    public async Task<Guid> Create(PlanetModel planet)
    {
        planet.Id = Guid.NewGuid();
        var planetEntity = _mapper.Map<PlanetEntity>(planet);
        return await _planetRepository.Save(planetEntity);
    }

    public async Task<Guid> Update(PlanetModel planet)
    {
        if (planet.Id != null)
        {
            var planetEntity = await _planetRepository.GetById(planet.Id.Value);
            if (planetEntity == null)
                throw new KeyNotFoundException("Такой планеты не существует");
            if(planet.UserId == null)
                planet.UserId = planetEntity.UserId;
            else if (planet.UserId != planetEntity.UserId)
                throw new IncorrectAuthorException("У вас нет прав на редактирование данной статьи");
            planetEntity = _mapper.Map<PlanetEntity>(planet);
            return await _planetRepository.Save(planetEntity);
        }
        else
        {
            throw new NoNullAllowedException();
        }
    }

    public async Task Delete(Guid planetId)
    {
        var planetEntity = await _planetRepository.GetById(planetId);
        if (planetEntity == null)
            throw new KeyNotFoundException("Такой звезды не существует");
        await _planetRepository.Delete(planetId);
    }
    public async Task Delete(Guid planetId, Guid userId)
    {
        var planetEntity = await _planetRepository.GetById(planetId);
        if (planetEntity == null)
            throw new KeyNotFoundException("Такой звезды не существует");
        if (planetEntity.UserId != userId)
            throw new IncorrectAuthorException("У вас нет прав на редактирование данной статьи");
        await _planetRepository.Delete(planetId);
    }
}