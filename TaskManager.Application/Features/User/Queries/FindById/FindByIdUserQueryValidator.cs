using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.User.Queries.FindById;

public sealed class FindByIdUserQueryValidator : AbstractValidator<FindByIdUserQuery>
{
    public FindByIdUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode(UserValidationError.IdRequired);
    }
}
