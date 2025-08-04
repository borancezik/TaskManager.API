using TaskManager.Domain.Entities.Base;

namespace TaskManager.Application.Interfaces.Repositories.Base;

public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    ValueTask<TEntity> AddAsync(TEntity entity);
    ValueTask<TEntity> UpdateAsync(TEntity entity);
    ValueTask<TEntity> FindByIdAsync(Guid Id);
}
