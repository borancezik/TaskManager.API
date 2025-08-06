using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Repositories.Base;

namespace TaskManager.Infrastructure.Repositories;

public class ProjectRepository : EfRepositoryBase<ProjectEntity>, IProjectRepository
{
    public ProjectRepository(TaskManagerContext context) : base(context)
    {
    }
}
