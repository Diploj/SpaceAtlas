using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.BL.Planet.Entities;

public class PlanetModel
{
    public Guid? Id { get; set; }
    public string? Name { get; set; } 
    public Kind Type { get; set; }
    public string? Description{ get; set; }
    public byte[]? BlobData { get; set; }
    public Guid? UserId{ get; set; }
    public Guid StarId { get; set; }
    public bool HasAtmosphere { get; set; }
}