using Application.Dtos.UserDtos;
using Domain.Abstractions;
using FluentValidation;

namespace Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator(IUserRepository userRepository)
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("Invalid email Address")
            .MustAsync(async (Email, _) =>
            {
                return await userRepository.IsEmailUniqueAsync(Email);
            }).WithMessage("Invalid email address");
    }
}
