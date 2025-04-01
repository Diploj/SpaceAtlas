using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.Controllers.Star.Entities;

public class StarResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public Kind Type { get; set; } = Kind.Star;
    public string? Description{ get; set; }
    public byte[]? BlobData { get; set; }
    public Guid UserId{ get; set; }
    public int Temperature{ get; set; }
}