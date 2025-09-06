using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.TeamMember.Commands.Update;

public sealed class UpdateTeamMemberCommandValidator : AbstractValidator<UpdateTeamMemberCommand>
{
    public UpdateTeamMemberCommandValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
          .WithErrorCode(TeamMemberValidationError.IdRequired);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(TeamMemberValidationError.NameRequired)
            .MaximumLength(100).WithMessage(TeamMemberValidationError.NameTooLong);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(TeamMemberValidationError.EmailRequired)
            .EmailAddress().WithMessage(TeamMemberValidationError.EmailInvalid)
            .MaximumLength(150).WithMessage(TeamMemberValidationError.EmailTooLong);

        RuleFor(x => x.Role)
            .MaximumLength(50).WithMessage(TeamMemberValidationError.RoleTooLong)
            .When(x => !string.IsNullOrWhiteSpace(x.Role));

        RuleFor(x => x.JoinedAt)
            .NotEmpty().WithMessage(TeamMemberValidationError.JoinedAtRequired)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(TeamMemberValidationError.JoinedAtInFuture);
    }
}
