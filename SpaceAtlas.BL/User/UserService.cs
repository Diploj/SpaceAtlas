using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SpaceAtlas.BL.User.Entities;
using SpaceAtlas.DataAccess.Entities;
using SpaceAtlas.DataAccess.Repository;

namespace SpaceAtlas.BL.User;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UserService(UserManager<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> GetById(Guid userId)
    {
        var user = await _userRepository.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new KeyNotFoundException("");
        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> GetByName(string userName)
    {
        var user = await _userRepository.FindByNameAsync(userName);
        if (user == null)
            throw new KeyNotFoundException("");
        return _mapper.Map<UserModel>(user);
    }

    public async Task<IList<string>> GetRole(UserModel user)
    {
        var role = await _userRepository.GetRolesAsync(_mapper.Map<UserEntity>(user));
        return role;
    }

    public async Task<bool> CheckPassword(UserModel user, string password)
    {
        return await _userRepository.CheckPasswordAsync(_mapper.Map<UserEntity>(user), password);
    }

    public async Task<IdentityResult> Create(UserModel user, string password)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        var result = await _userRepository.CreateAsync(userEntity, password);
        await _userRepository.AddToRoleAsync(userEntity, "User");
        return result;
    }

    public async Task<IdentityResult> Update(UserModel user, string password)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        userEntity.PasswordHash = _userRepository.PasswordHasher?.HashPassword(userEntity, password);
        var result = await _userRepository.UpdateAsync(userEntity);
        return result;
    }

    public async Task<IdentityResult> Delete(Guid userId)
    {
        try
        {
            var user = await _userRepository.FindByIdAsync(userId.ToString()); 
            if (user == null)
                throw new KeyNotFoundException("adaw");
            var result = await _userRepository.DeleteAsync(user);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Я бля хз");
        }
        
    }
}