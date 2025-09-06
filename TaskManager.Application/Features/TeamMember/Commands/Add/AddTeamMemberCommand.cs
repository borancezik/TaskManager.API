using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.TeamMember.Commands.Add;

public sealed class AddTeamMemberCommand : IRequest<Result<AddTeamMemberCommandResponse>>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
