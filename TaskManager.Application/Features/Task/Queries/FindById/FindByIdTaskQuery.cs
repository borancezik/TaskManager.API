using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Task.Queries.FindById;

public sealed class FindByIdTaskQuery : IRequest<Result<FindByIdTaskQueryResponse>>  
{
    public Guid Id { get; set; }
}
