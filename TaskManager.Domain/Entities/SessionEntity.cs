using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Entities;

[Table("session", Schema = "task_manager")]
public class SessionEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public string AccessToken { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
