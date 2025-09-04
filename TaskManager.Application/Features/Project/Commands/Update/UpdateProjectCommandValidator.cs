using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.Project.Commands.Update;

internal sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithErrorCode(ProjectValidationError.ProjectIdRequired);

        RuleFor(x => x.Name)
          .NotEmpty()
          .WithErrorCode(ProjectValidationError.ProjectNameRequired)
          .MaximumLength(100)
          .WithErrorCode(ProjectValidationError.ProjectNameTooLong);

        RuleFor(x => x.StartDate)
            .NotNull()
            .WithErrorCode(ProjectValidationError.StartDateRequired);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue && x.StartDate.HasValue)
            .WithErrorCode(ProjectValidationError.EndDateGreaterThanStartDate);
    }
}
