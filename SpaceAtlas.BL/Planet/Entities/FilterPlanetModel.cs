namespace SpaceAtlas.BL.Planet.Entities;

public class FilterPlanetModel
{
    public string? Name { get; set; } 
    public Guid? UserId{ get; set; }
    public Guid? StarId { get; set; }
    public bool? HasAtmosphere { get; set; }
}