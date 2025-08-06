using TaskManager.Application.Dtos.Base;
using TaskManager.Application.Interfaces.Repositories.Base;
using TaskManager.Application.Utilities.Result;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Application.Interfaces.Services.Base;

public interface IBaseService<TEntity, TRepository, TResponseDto, TDto>
    where TEntity : BaseEntity
    where TRepository : IRepositoryBase<TEntity>
    where TResponseDto : BaseResponseDto
    where TDto : BaseDto
{
    ValueTask<Result<TResponseDto>> AddAsync(TDto dto);
    ValueTask<Result<TResponseDto>> UpdateAsync(TDto dto);
    ValueTask<Result<TDto>> FindByIdAsync(Guid id);
}
