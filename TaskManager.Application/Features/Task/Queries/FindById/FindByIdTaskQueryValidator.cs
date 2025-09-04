using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.Task.Queries.FindById;

internal sealed class FindByIdTaskQueryValidator : AbstractValidator<FindByIdTaskQuery>
{
    public FindByIdTaskQueryValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty()
         .WithErrorCode(TaskValidationError.IdRequired);
    }
}
