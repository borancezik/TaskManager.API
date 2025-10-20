using TaskManager.Application.Dtos.Session;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Services.Base;

namespace TaskManager.Infrastructure.Services;

public class SessionService : BaseService<SessionEntity, ISessionRepository, SessionResponseDto, SessionDto>, ISessionService
{
    public SessionService(ISessionRepository repositoryBase) : base(repositoryBase)
    {
    }
}
