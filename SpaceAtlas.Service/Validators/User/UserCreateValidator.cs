using FluentValidation;
using SpaceAtlas.Controllers.User.Entities;

namespace SpaceAtlas.Validators.User;

public class UserCreateValidator: AbstractValidator<UserCreateRequest>
{
    public UserCreateValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage("Username is required");
        RuleFor(u => u.Password)
            .Length(6, 20)
            .WithMessage("Password must be between 6 and 20 characters");
    }
}