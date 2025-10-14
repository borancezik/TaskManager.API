using TaskManager.Application.Dtos.Task;
using TaskManager.Application.Interfaces.Persistence.Repositories;
using TaskManager.Application.Interfaces.Services.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Services;

public interface ITaskService : IBaseService<TaskEntity, ITaskRepository, TaskResponseDto, TaskDto>
{
}
