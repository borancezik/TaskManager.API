using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Task.Commands.Update;

public sealed class UpdateTaskCommand : IRequest<Result<UpdateTaskCommandResponse>>
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public Guid? AssignedUserId { get; set; }
}
