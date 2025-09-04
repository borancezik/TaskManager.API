using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.Project.Queries.FindById;

internal sealed class FindByIdProjectQueryValidator : AbstractValidator<FindByIdProjectQuery>
{
    public FindByIdProjectQueryValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty()
          .WithErrorCode(ProjectValidationError.ProjectIdRequired);
    }
}
