using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Repositories.Base;

namespace TaskManager.Infrastructure.Repositories;

public class TeamMemberRepository : EfRepositoryBase<TeamMemberEntity>, ITeamMemberRepository
{
    public TeamMemberRepository(TaskManagerContext context) : base(context)
    {
    }
}
