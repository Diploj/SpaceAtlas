using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SpaceAtlas.DataAccess.Entities;

[Table("users")]
public class UserEntity : IdentityUser<Guid>, IBaseEntity
{
    public virtual ICollection<StarEntity> Stars { get; set; }
    public virtual ICollection<PlanetEntity> Planets { get; set; }
}