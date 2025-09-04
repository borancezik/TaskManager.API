using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Task.Commands.Add;

public sealed class AddTaskCommand : IRequest<Result<AddTaskCommandResponse>>   
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public Guid? AssignedUserId { get; set; }
}
