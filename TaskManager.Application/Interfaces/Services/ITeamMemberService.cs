using TaskManager.Application.Dtos.TeamMember;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Services;

public interface ITeamMemberService : IBaseService<TeamMemberEntity, ITeamMemberRepository, TeamMemberResponseDto, TeamMemberDto>
{
}
