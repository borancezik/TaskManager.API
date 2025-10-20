using Mapster;
using System.Linq.Expressions;
using TaskManager.Application.Dtos.Base;
using TaskManager.Application.Interfaces.Persistence.Repositories.Base;
using TaskManager.Application.Interfaces.Services.Base;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Infrastructure.Services.Base;

public class BaseService<TEntity, TRepository, TResponseDto, TDto> : IBaseService<TEntity, TRepository, TResponseDto, TDto>
    where TEntity : BaseEntity
    where TRepository : IRepositoryBase<TEntity>
    where TResponseDto : BaseResponseDto
    where TDto : BaseDto
{
    private readonly TRepository _repositoryBase;

    public BaseService(TRepository repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result<TResponseDto>> AddAsync(TDto dto)
    {
        var entity = dto.Adapt<TEntity>();

        var addedEntity = await _repositoryBase.AddAsync(entity);

        if (addedEntity is null)
            return Error.AddedError;

        return addedEntity.Adapt<TResponseDto>();
    }

    public async Task<Result<TDto>> FindByIdAsync(Guid id)
    {
        var entity = await _repositoryBase.FindByIdAsync(id);

        if (entity is null)
            return Error.NotFound;

        return entity.Adapt<TDto>();
    }

    public async Task<Result<TResponseDto>> UpdateAsync(TDto dto)
    {
        var entity = dto.Adapt<TEntity>();

        var updatedEntity = await _repositoryBase.UpdateAsync(entity);

        if (updatedEntity is null)
            return Error.UpdatedError;

        return updatedEntity.Adapt<TResponseDto>();
    }

    public async Task<Result<TDto>> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _repositoryBase.GetByExpressionAsync(expression);

        if (entity is null)
            return Error.NotFound;

        return entity.Adapt<TDto>();
    }

    public async Task<List<TDto>> GetListAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _repositoryBase.GetListAsync(expression);

        return entity.Adapt<List<TDto>>();
    }

    public async ValueTask<Result<bool>> DeleteByIdAsync(Guid id)
    {
        var deletedEntity = await _repositoryBase.DeleteByIdAsync(id);

        if (!deletedEntity)
            return Error.DeletedError;

        return deletedEntity;
    }
}
