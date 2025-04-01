using System.Data;
using AutoMapper;
using SpaceAtlas.BL.User.Entities;
using SpaceAtlas.DataAccess.Entities;
using SpaceAtlas.DataAccess.Repository;

namespace SpaceAtlas.BL.User;

public class UserService : IUserService
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public IEnumerable<UserModel> GetAll(FilterUserModel? filter = null)
    {
        var users = _userRepository.GetAll(x =>
            filter == null || 
            (filter.Username == null || x.Username == filter.Username));
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GetById(Guid userId)
    {
        var user = _userRepository.GetById(userId);
        if(user == null)
            throw new KeyNotFoundException();
        return _mapper.Map<UserModel>(user);
    }

    public Guid Create(UserModel user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        return _userRepository.Save(userEntity);
    }

    public Guid Update(UserModel user)
    {
        if (user.Id != null)
        {
            var userEntity = _userRepository.GetById(user.Id.Value);
            if (userEntity == null)
                throw new KeyNotFoundException();
            userEntity = _mapper.Map<UserEntity>(user);
            return _userRepository.Save(userEntity);
        }
        else
        {
            throw new NoNullAllowedException();
        }
    }

    public void Delete(Guid userId)
    {
        _userRepository.Delete(userId);
    }
}