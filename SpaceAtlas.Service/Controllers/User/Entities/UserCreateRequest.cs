namespace SpaceAtlas.Controllers.User.Entities;

public class UserCreateRequest
{
    public string UserName { get; set; }
    
    public string? Email { get; set; }
    public string Password { get; set; }
}