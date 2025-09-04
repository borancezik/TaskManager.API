using MediatR;
using TaskManager.Application.Dtos.Task;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Task.Commands.Add;

internal sealed class AddTaskCommandHandler(ITaskService taskService) : IRequestHandler<AddTaskCommand, Result<AddTaskCommandResponse>>
{
    private readonly ITaskService _taskService = taskService;   
    public async Task<Result<AddTaskCommandResponse>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var taskDto = new TaskDto
        {
            Title = request.Title,
            ProjectId = request.ProjectId,
            Description = request.Description,
            Status = request.Status,
            Priority = request.Priority,
            DueDate = request.DueDate,
            AssignedUserId = request.AssignedUserId
        };

        var addedTask =  await _taskService.AddAsync(taskDto);

        if (!addedTask.IsSuccess)
            return Error.AddedError;

        return new AddTaskCommandResponse
        {
            Id = addedTask.Data.Id.Value,
        };
    }
}
