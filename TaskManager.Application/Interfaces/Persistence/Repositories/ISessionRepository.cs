using TaskManager.Application.Interfaces.Persistence.Repositories.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Persistence.Repositories;

public interface ISessionRepository : IRepositoryBase<SessionEntity>;
