using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Task.Commands.Update;

internal sealed class UpdateTaskCommandHandler(ITaskService taskService) : IRequestHandler<UpdateTaskCommand, Result<UpdateTaskCommandResponse>>
{
    private readonly ITaskService _taskService = taskService;
    public async Task<Result<UpdateTaskCommandResponse>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskDto = await _taskService.FindByIdAsync(request.Id);

        if (!taskDto.IsSuccess)
            return Error.NotFound;

        var task = taskDto.Data;

        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.Priority = request.Priority;
        task.ProjectId = request.ProjectId;
        task.AssignedUserId = request.AssignedUserId;
        task.Status = request.Status;


        var updatedTask = await _taskService.UpdateAsync(task);

        if (!updatedTask.IsSuccess)
            return Error.UpdatedError;

        return new UpdateTaskCommandResponse();
    }
}
