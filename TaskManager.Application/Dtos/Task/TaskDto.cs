using TaskManager.Application.Dtos.Base;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Dtos.Task;

public class TaskDto : BaseDto
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public Guid? AssignedUserId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
}
