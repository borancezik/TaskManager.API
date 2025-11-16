using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Application.Interfaces.Persistence.Context;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Infrastructure.Persistence.Context;

public class TaskManagerContext : DbContext, ITaskManagerContext
{
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, "IsDeleted");
                var constant = Expression.Constant(false, typeof(bool));
                var expression = Expression.Equal(property, constant);
                var lambda = Expression.Lambda(expression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }

    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}
