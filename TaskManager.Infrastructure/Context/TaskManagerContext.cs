using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces.Context;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Context;

public class TaskManagerContext : DbContext, ITaskManagerContext
{
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
    {

    }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TeamMemberEntity> TeamMembers { get; set; }
}
