using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Infrastructure.Persistence.Repositories.Base;

namespace TaskManager.Infrastructure.Persistence.Repositories;

public class TaskRepository : EfRepositoryBase<TaskEntity>, ITaskRepository
{
    public TaskRepository(TaskManagerContext context) : base(context)
    {
    }
}
