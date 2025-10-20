using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Infrastructure.Persistence.Repositories.Base;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class SessionRepository : EfRepositoryBase<SessionEntity>, ISessionRepository
{
    public SessionRepository(TaskManagerContext context) : base(context)
    {
    }
}
