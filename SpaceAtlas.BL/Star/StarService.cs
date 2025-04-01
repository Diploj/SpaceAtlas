using System.Data;
using AutoMapper;
using SpaceAtlas.BL.Star.Entities;
using SpaceAtlas.DataAccess.Entities;
using SpaceAtlas.DataAccess.Repository;

namespace SpaceAtlas.BL.Star;

public class StarService : IStarService
{
    private readonly IRepository<StarEntity> _starRepository;
    private readonly IMapper _mapper;

    public StarService(IRepository<StarEntity> starRepository, IMapper mapper)
    {
        _starRepository = starRepository;
        _mapper = mapper;
    }
    
    public IEnumerable<StarModel> GetAll(FilterStarModel? filter = null)
    {
        var stars = _starRepository.GetAll(x =>
            filter == null || 
            (filter.Name == null || x.Name  == filter.Name )||
            (filter.UserId == null || x.UserId == filter.UserId));
        return _mapper.Map<IEnumerable<StarModel>>(stars);
    }

    public StarModel GetById(Guid starId)
    {
        var star = _starRepository.GetById(starId);
        if(star == null)
            throw new KeyNotFoundException();
        return _mapper.Map<StarModel>(star);
    }

    public Guid Create(StarModel star)
    {
        star.Id = Guid.NewGuid();
        var starEntity = _mapper.Map<StarEntity>(star);
        return _starRepository.Save(starEntity);
    }

    public Guid Update(StarModel star)
    {
        if (star.Id != null)
        {
            var starEntity = _starRepository.GetById(star.Id.Value);
            if (starEntity == null)
                throw new KeyNotFoundException();
            starEntity = _mapper.Map<StarEntity>(star);
            return _starRepository.Save(starEntity);
        }
        else
        {
            throw new NoNullAllowedException();
        }
    }

    public void Delete(Guid starId)
    {
        _starRepository.Delete(starId);
    }
}