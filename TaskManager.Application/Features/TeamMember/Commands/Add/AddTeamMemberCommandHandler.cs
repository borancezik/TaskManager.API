using MediatR;
using TaskManager.Application.Dtos.TeamMember;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.TeamMember.Commands.Add;

internal sealed class AddTeamMemberCommandHandler(ITeamMemberService teamMemberService) : IRequestHandler<AddTeamMemberCommand, Result<AddTeamMemberCommandResponse>>
{
    private readonly ITeamMemberService _teamMemberService = teamMemberService;
    public async Task<Result<AddTeamMemberCommandResponse>> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var teamMemberDto = new TeamMemberDto
        {
            Name = request.Name,
            Email = request.Email,
            Role = request.Role,
            JoinedAt = request.JoinedAt
        };

        var addedTeamMember = await _teamMemberService.AddAsync(teamMemberDto);

        if (!addedTeamMember.IsSuccess)
            return Error.AddedError;

        return new AddTeamMemberCommandResponse
        {
            Id = addedTeamMember.Data.Id.Value,
        };
    }
}
