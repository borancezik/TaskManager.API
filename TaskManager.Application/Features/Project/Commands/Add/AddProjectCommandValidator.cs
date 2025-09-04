using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.Project.Commands.Add;

internal sealed class AddProjectCommandValidator : AbstractValidator<AddProjectCommand>
{
    public AddProjectCommandValidator()
    {
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
