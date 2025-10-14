namespace TaskManager.Application.Features.User.Queries.FindById;

public sealed class FindByIdUserQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
