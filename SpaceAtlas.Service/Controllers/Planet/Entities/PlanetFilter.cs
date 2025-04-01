using SpaceAtlas.DataAccess.Entities;

namespace SpaceAtlas.Controllers.Planet.Entities;

public class PlanetFilter
{
    public string? Name { get; set; } 
    public Guid? UserId{ get; set; }
    public Guid? StarId { get; set; }
    public bool? HasAtmosphere { get; set; }
}