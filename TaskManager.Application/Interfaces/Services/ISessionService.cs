using TaskManager.Application.Dtos.Session;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Services;

public interface ISessionService : IBaseService<SessionEntity, ISessionRepository, SessionResponseDto, SessionDto>;
