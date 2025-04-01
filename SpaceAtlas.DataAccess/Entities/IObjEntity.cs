namespace SpaceAtlas.DataAccess.Entities;

public enum Kind
{
    Star,
    Planet,
    Galactic,
    Another
}

public interface IObjEntity : IBaseEntity
{
    string Name { get; set; } 
    Kind Type { get; set; } 
    string Description { get; set; }
    byte[]? BlobData { get; set; }
    Guid UserId { get; set; }
    UserEntity User { get; set; }
}