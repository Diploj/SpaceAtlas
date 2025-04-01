using FluentValidation;
using SpaceAtlas.Controllers.Planet.Entities;

namespace SpaceAtlas.Validators.Planet;

public class PlanetCreateValidator : AbstractValidator<PlanetCreateRequest>
{
    public PlanetCreateValidator()
    {
        
    }
}