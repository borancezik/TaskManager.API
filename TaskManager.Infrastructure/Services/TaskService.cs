using TaskManager.Application.Dtos.Task;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Services.Base;

namespace TaskManager.Infrastructure.Services;

public class TaskService : BaseService<TaskEntity, ITaskRepository, TaskResponseDto, TaskDto>, ITaskService
{
    public TaskService(ITaskRepository repositoryBase) : base(repositoryBase)
    {
    }
}
