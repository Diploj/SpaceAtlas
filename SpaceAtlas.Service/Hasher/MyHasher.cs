using Microsoft.AspNetCore.Identity;
namespace SpaceAtlas.Hasher;

public class MyHasher
{
    public static string Hash(string password)
    {
        var hasher = new PasswordHasher<IdentityUser>();
        return hasher.HashPassword(null, password);
    }
}