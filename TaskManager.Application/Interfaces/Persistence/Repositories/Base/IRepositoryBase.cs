using System.Linq.Expressions;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Application.Interfaces.Persistence.Repositories.Base;

public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    ValueTask<bool> DeleteByIdAsync(Guid id);
    Task<TEntity> FindByIdAsync(Guid Id);
    Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression);
}
