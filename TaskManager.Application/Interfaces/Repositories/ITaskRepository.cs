using TaskManager.Application.Interfaces.Repositories.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Repositories;

public interface ITaskRepository : IRepositoryBase<TaskEntity>;
