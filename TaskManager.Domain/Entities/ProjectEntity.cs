using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Entities;

[Table("projects")]
public class ProjectEntity : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("start_date")]
    public DateTime? StartDate { get; set; }

    [Column("end_date")]
    public DateTime? EndDate { get; set; }

    [Column("is_archived")]
    public bool? IsArchived { get; set; }
}
