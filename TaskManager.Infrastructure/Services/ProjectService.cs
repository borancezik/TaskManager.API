using TaskManager.Application.Dtos.Project;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Services.Base;

namespace TaskManager.Infrastructure.Services;

public class ProjectService : BaseService<ProjectEntity, IProjectRepository, ProjectResponseDto, ProjectDto>, IProjectService
{
    public ProjectService(IProjectRepository repositoryBase) : base(repositoryBase)
    {
    }
}
