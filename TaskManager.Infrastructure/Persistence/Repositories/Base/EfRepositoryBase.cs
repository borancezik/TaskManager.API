﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Application.Interfaces.Persistence.Repositories.Base;
using TaskManager.Domain.Entities.Base;
using TaskManager.Infrastructure.Persistence.Context;

namespace TaskManager.Infrastructure.Persistence.Repositories.Base;

public class EfRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;
    protected readonly TaskManagerContext _context;
    public EfRepositoryBase(TaskManagerContext context)
    {
        _dbSet = context.Set<TEntity>();
        _context = context;
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity);

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> FindByIdAsync(Guid Id)
    {
        return await _dbSet.FirstAsync(x => x.Id == Id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.FirstAsync(expression);
    }
}
