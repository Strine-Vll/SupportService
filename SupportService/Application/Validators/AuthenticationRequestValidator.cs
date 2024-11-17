using Application.Dtos.UserDtos;
using FluentValidation;

namespace Application.Validators;

public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(a => a.Email).EmailAddress().WithMessage("Invalid email address");
        RuleFor(a => a.Password).NotEmpty().WithMessage("Password is required");
    }
}
