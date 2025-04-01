namespace SpaceAtlas.BL.User.Entities;

public class UserModel
{
    public Guid? Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}