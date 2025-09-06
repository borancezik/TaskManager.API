using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Domain.Entities.Base;

public class BaseEntity
{
    [Column("id")]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

}
