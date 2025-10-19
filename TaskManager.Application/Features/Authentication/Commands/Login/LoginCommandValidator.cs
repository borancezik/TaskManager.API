using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.Authentication.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
        .NotEmpty().WithMessage(UserValidationError.UserNameRequired);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(UserValidationError.PasswordRequired);
    }
}
