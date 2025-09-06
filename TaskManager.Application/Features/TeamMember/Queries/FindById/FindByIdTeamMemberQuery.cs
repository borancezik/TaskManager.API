using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.TeamMember.Queries.FindById;

public sealed class FindByIdTeamMemberQuery : IRequest<Result<FindByIdTeamMemberQueryResponse>> 
{
    public Guid Id { get; set; }
}
