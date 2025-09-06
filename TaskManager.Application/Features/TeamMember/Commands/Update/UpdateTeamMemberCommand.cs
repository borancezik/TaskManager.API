using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.TeamMember.Commands.Update;

public sealed class UpdateTeamMemberCommand : IRequest<Result<UpdateTeamMemberCommandResponse>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
