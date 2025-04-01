using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAtlas.DataAccess.Entities;

[Table("stars")]
public class StarEntity: IObjEntity
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; } 
    public Kind Type{ get; set; }
    public string? Description{ get; set; }
    [Column(TypeName = "bytea")] 
    public byte[]? BlobData { get; set; }
    public Guid UserId{ get; set; }
    public UserEntity User { get; set; }
    public int Temperature{ get; set; }
    public virtual ICollection<PlanetEntity> Planets { get; set; }
}