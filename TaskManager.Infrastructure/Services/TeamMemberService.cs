using TaskManager.Application.Dtos.TeamMember;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Services.Base;

namespace TaskManager.Infrastructure.Services;

public class TeamMemberService : BaseService<TeamMemberEntity, ITeamMemberRepository, TeamMemberResponseDto, TeamMemberDto>, ITeamMemberService
{
    public TeamMemberService(ITeamMemberRepository repositoryBase) : base(repositoryBase)
    {
    }
}
