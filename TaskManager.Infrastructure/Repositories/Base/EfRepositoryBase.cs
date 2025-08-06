using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces.Repositories.Base;
using TaskManager.Domain.Entities.Base;
using TaskManager.Infrastructure.Context;

namespace TaskManager.Infrastructure.Repositories.Base;

public class EfRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;
    protected readonly TaskManagerContext _context;
    public EfRepositoryBase(TaskManagerContext context)
    {
        _dbSet = context.Set<TEntity>();
        _context = context;
    }
    public async ValueTask<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity);

        await _context.SaveChangesAsync();

        return entity;
    }

    public async ValueTask<TEntity> FindByIdAsync(Guid Id)
    {
        return await _dbSet.FirstAsync(x => x.Id == Id);
    }

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return updatedEntity.Entity;
    }
}
