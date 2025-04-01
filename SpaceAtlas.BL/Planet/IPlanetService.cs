using SpaceAtlas.BL.Planet.Entities;

namespace SpaceAtlas.BL.Planet;

public interface IPlanetService
{
    public IEnumerable<PlanetModel> GetAll(FilterPlanetModel? filter = null);
    public PlanetModel GetById(Guid planetId);
    public Guid Create(PlanetModel planet);
    public Guid Update(PlanetModel planet);
    public void Delete(Guid planetId);
}