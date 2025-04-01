using SpaceAtlas.BL.Star.Entities;

namespace SpaceAtlas.BL.Star;

public interface IStarService
{
    public IEnumerable<StarModel> GetAll(FilterStarModel? filter = null);
    public StarModel GetById(Guid starId);
    public Guid Create(StarModel star);
    public Guid Update(StarModel star);
    public void Delete(Guid starId);
}