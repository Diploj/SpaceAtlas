using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SpaceAtlas.DataAccess.Entities;

[Table("users")]
public class UserEntity : IdentityUser<Guid>, IBaseEntity
{
    public string Username { get; set; }
    public string? Email { get; set; }
    public string PasswordHash { get; set; }
    public virtual ICollection<StarEntity> Stars { get; set; }
    public virtual ICollection<PlanetEntity> Planets { get; set; }
}