using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Entities;

[Table("team_members", Schema = "task_manager")]
public class UserEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("role")]
    public string? Role { get; set; }

    [Column("joined_at")]
    public DateTime JoinedAt { get; set; }
}
