using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceAtlas.DataAccess.Entities;
[Table("planets")]
public class PlanetEntity:IObjEntity
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; } 
    public Kind Type { get; set; }
    public string? Description{ get; set; }
    [Column(TypeName = "bytea")] 
    public byte[]? BlobData { get; set; }
    public Guid UserId{ get; set; }
    public UserEntity User{ get; set; }
    public Guid StarId { get; set; }
    public StarEntity Star { get; set; }
    public bool HasAtmosphere { get; set; }
}