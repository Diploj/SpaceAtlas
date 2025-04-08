using SpaceAtlas.BL.Planet.Entities;

namespace SpaceAtlas.BL.Planet;

public interface IPlanetService
{
    public Task<IEnumerable<PlanetModel>> GetAll(FilterPlanetModel? filter = null);
    public Task<PlanetModel> GetById(Guid planetId);
    public Task<Guid> Create(PlanetModel planet);
    public Task<Guid> Update(PlanetModel planet);
    public Task Delete(Guid planetId);
    public Task Delete(Guid planetId,Guid userId);
}