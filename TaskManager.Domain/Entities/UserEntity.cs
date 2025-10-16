using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Entities;

[Table("user", Schema = "task_manager")]
public class UserEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("username")]
    public string Username { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [Column("password_salt")]
    public string PasswordSalt { get; set; } = null!;

    [Column("iterations")]
    public int Iterations { get; set; }

    [Column("joined_at")]
    public DateTime? JoinedAt { get; set; } = DateTime.UtcNow;
}
