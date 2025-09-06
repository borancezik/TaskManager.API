using Mapster;
using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.TeamMember.Queries.FindById;

internal sealed class FindByIdTeamMemberQueryHandler(ITeamMemberService teamMemberService) : IRequestHandler<FindByIdTeamMemberQuery, Result<FindByIdTeamMemberQueryResponse>>
{
    private readonly ITeamMemberService _teamMemberService = teamMemberService;
    public async Task<Result<FindByIdTeamMemberQueryResponse>> Handle(FindByIdTeamMemberQuery request, CancellationToken cancellationToken)
    {
        var teamMember = await _teamMemberService.FindByIdAsync(request.Id);

        if (!teamMember.IsSuccess)
            return Error.NotFound;

        return teamMember.Data.Adapt<FindByIdTeamMemberQueryResponse>();
    }
}
