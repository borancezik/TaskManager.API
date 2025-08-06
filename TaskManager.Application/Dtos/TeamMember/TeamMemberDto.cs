using TaskManager.Application.Dtos.Base;

namespace TaskManager.Application.Dtos.TeamMember;

public class TeamMemberDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
