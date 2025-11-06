using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.Authentication.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(UserValidationError.NameRequired)
            .MaximumLength(100).WithMessage(UserValidationError.NameTooLong);

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(UserValidationError.UserNameRequired)
            .MaximumLength(100).WithMessage(UserValidationError.UserNameTooLong);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(UserValidationError.EmailRequired)
            .EmailAddress().WithMessage(UserValidationError.EmailInvalid)
            .MaximumLength(150).WithMessage(UserValidationError.EmailTooLong);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(UserValidationError.PasswordRequired);
    }
}
