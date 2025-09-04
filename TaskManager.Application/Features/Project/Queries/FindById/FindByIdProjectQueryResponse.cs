namespace TaskManager.Application.Features.Project.Queries.FindById;

public sealed class FindByIdProjectQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsArchived { get; set; }
}
