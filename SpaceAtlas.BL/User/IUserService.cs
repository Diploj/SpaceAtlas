using Microsoft.AspNetCore.Identity;
using SpaceAtlas.BL.User.Entities;
using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.BL.User;

public interface IUserService
{
    //public IEnumerable<UserModel> GetAll(FilterUserModel? filter = null);
    public Task<UserModel> GetById(Guid userId);
    public Task<UserModel> GetByName(string username);
    public Task<IList<string>> GetRole(UserModel user);
    public Task<bool> CheckPassword(UserModel user, string password);
    public Task<IdentityResult> Create(UserModel user,string password);
    public Task<IdentityResult> Update(UserModel user,string password);
    public Task<IdentityResult> Delete(Guid userId);
}