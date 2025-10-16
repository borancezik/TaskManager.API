using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.User.Commands.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
          .WithErrorCode(UserValidationError.IdRequired);

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

        RuleFor(x => x.JoinedAt)
            .NotEmpty().WithMessage(UserValidationError.JoinedAtRequired)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(UserValidationError.JoinedAtInFuture);
    }
}
