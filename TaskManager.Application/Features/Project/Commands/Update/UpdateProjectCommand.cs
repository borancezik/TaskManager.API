using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Project.Commands.Update;

public sealed class UpdateProjectCommand : IRequest<Result<UpdateProjectCommandResponse>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsArchived { get; set; }
}
