namespace SpaceAtlas.Controllers.User.Entities;

public class UserUpdateRequest
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}