using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Repositories.Base;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : EfRepositoryBase<TaskEntity>, ITaskRepository
{
    public TaskRepository(TaskManagerContext context) : base(context)
    {
    }
}
