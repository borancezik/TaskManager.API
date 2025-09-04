using Mapster;
using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Task.Queries.FindById;

internal sealed class FindByIdTaskQueryHandler(ITaskService taskService) : IRequestHandler<FindByIdTaskQuery, Result<FindByIdTaskQueryResponse>>
{
    private readonly ITaskService _taskService = taskService;
    public async Task<Result<FindByIdTaskQueryResponse>> Handle(FindByIdTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskService.FindByIdAsync(request.Id);

        if (!task.IsSuccess)
            return Error.NotFound;

        return task.Data.Adapt<FindByIdTaskQueryResponse>();
    }
}
