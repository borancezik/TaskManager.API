using TaskManager.Domain.Entities.Base;

namespace TaskManager.Application.Interfaces.Repositories.Base;

public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> FindByIdAsync(Guid Id);
}
