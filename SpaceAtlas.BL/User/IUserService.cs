using SpaceAtlas.BL.User.Entities;
namespace SpaceAtlas.BL.User;

public interface IUserService
{
    public IEnumerable<UserModel> GetAll(FilterUserModel? filter = null);
    public UserModel GetById(Guid userId);
    public Guid Create(UserModel user);
    public Guid Update(UserModel user);
    public void Delete(Guid userId);
}