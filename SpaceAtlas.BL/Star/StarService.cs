using System.Data;
using AutoMapper;
using SpaceAtlas.BL.Exceptions;
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
    
    public async Task<IEnumerable<StarModel>> GetAll(FilterStarModel? filter = null)
    {
        var stars = await _starRepository.GetAll(x =>
            filter == null || 
            (filter.Name == null || x.Name  == filter.Name )||
            (filter.UserId == null || x.UserId == filter.UserId));
        return _mapper.Map<IEnumerable<StarModel>>(stars);
    }

    public async Task<StarModel> GetById(Guid starId)
    {
        var star = await _starRepository.GetById(starId);
        if(star == null)
            throw new KeyNotFoundException();
        return _mapper.Map<StarModel>(star);
    }

    public async Task<Guid> Create(StarModel star)
    {
        star.Id = Guid.NewGuid();
        var starEntity = _mapper.Map<StarEntity>(star);
        return await _starRepository.Save(starEntity);
    }

    public async Task<Guid> Update(StarModel star)
    {
        if (star.Id != null)
        {
            var starEntity = await _starRepository.GetById(star.Id.Value);
            if (starEntity == null)
                throw new KeyNotFoundException("Такой звезды не существует");
            if (star.UserId == null)
                star.UserId = starEntity.UserId;
            else if (star.UserId != starEntity.UserId)
                throw new IncorrectAuthorException("У вас нет прав на редактирование данной статьи");
            starEntity = _mapper.Map<StarEntity>(star);
            return await _starRepository.Save(starEntity);
        }
        else
        {
            throw new NoNullAllowedException();
        }
    }

    public async Task Delete(Guid starId)
    {
        var starEntity = await _starRepository.GetById(starId);
        if (starEntity == null)
            throw new KeyNotFoundException("Такой звезды не существует");
        await _starRepository.Delete(starId);
    }
    
    public async Task Delete(Guid starId,Guid userId)
    {
        var starEntity = await _starRepository.GetById(starId);
        if (starEntity == null)
            throw new KeyNotFoundException("Такой звезды не существует");
        if (starEntity.UserId != userId)
            throw new IncorrectAuthorException("У вас нет прав на редактирование данной статьи");
        await _starRepository.Delete(starId);
    }
}