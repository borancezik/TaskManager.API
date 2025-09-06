using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.TeamMember.Commands.Update;

internal sealed class UpdateTeamMemberCommandHandler(ITeamMemberService teamMemberService) : IRequestHandler<UpdateTeamMemberCommand, Result<UpdateTeamMemberCommandResponse>>
{
    private readonly ITeamMemberService _teamMemberService = teamMemberService;
    public async Task<Result<UpdateTeamMemberCommandResponse>> Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var teamMemberDto = await _teamMemberService.FindByIdAsync(request.Id);

        if (!teamMemberDto.IsSuccess)
            return Error.NotFound;

        var teamMember = teamMemberDto.Data;
        
        teamMember.Name = request.Name;
        teamMember.Email = request.Email;
        teamMember.Role = request.Role;
        teamMember.JoinedAt = request.JoinedAt;

        var updatedteamMember = await _teamMemberService.UpdateAsync(teamMember);

        if (!updatedteamMember.IsSuccess)
            return Error.UpdatedError;

        return new UpdateTeamMemberCommandResponse();
    }
}
