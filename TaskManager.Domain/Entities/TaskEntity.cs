using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Entities;

[Table("tasks", Schema = "task_manager")]
public class TaskEntity : BaseEntity
{
    [Column("project_id")]
    public Guid ProjectId { get; set; }

    [Column("title")]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("status")]
    public string Status { get; set; }

    [Column("due_date")]
    public DateTime? DueDate { get; set; }

    [Column("priority")]
    public int Priority { get; set; }

    [Column("assigned_user_id")]
    public Guid? AssignedUserId { get; set; }

    [ForeignKey("project_id")]
    public ProjectEntity? Project { get; set; } = null;
}
