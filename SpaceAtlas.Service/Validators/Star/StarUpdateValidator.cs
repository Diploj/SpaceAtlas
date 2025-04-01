using FluentValidation;
using SpaceAtlas.Controllers.Star.Entities;

namespace SpaceAtlas.Validators.Star;
public class StarUpdateValidator: AbstractValidator<StarUpdateRequest>
{
    public StarUpdateValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
        RuleFor(s => s.Temperature)
            .GreaterThan(0);
    }
}