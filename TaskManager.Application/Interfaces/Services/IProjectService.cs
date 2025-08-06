using TaskManager.Application.Dtos.Project;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Application.Interfaces.Services.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces.Services;

public interface IProjectService : IBaseService<ProjectEntity, IProjectRepository, ProjectResponseDto, ProjectDto>
{
}
