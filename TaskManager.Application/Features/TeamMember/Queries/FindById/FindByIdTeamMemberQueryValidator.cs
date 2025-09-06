using FluentValidation;
using TaskManager.Application.Utilities.Errors.ValidationErrors;

namespace TaskManager.Application.Features.TeamMember.Queries.FindById;

public sealed class FindByIdTeamMemberQueryValidator : AbstractValidator<FindByIdTeamMemberQuery>
{
    public FindByIdTeamMemberQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode(TeamMemberValidationError.IdRequired);
    }
}
