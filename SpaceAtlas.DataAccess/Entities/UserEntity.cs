using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAtlas.DataAccess.Entities;

[Table("users")]
public class UserEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public virtual ICollection<StarEntity> Stars { get; set; }
    public virtual ICollection<PlanetEntity> Planets { get; set; }
}