using FluentValidation;
using SpaceAtlas.Controllers.Planet.Entities;

namespace SpaceAtlas.Validators.Planet;

public class PlanetUpdateValidator : AbstractValidator<PlanetUpdateRequest>
{
    public PlanetUpdateValidator()
    {
        
    }
}