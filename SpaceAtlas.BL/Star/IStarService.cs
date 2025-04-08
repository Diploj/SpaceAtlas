using SpaceAtlas.BL.Star.Entities;

namespace SpaceAtlas.BL.Star;

public interface IStarService
{
    public Task<IEnumerable<StarModel>> GetAll(FilterStarModel? filter = null);
    public Task<StarModel> GetById(Guid starId);
    public Task<Guid> Create(StarModel star);
    public Task<Guid> Update(StarModel star);
    //public Task<Guid> UpdateAdmin(StarModel star);
    public Task Delete(Guid starId);
    public Task Delete(Guid starId,Guid userId);
}