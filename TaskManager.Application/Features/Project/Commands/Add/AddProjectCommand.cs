using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Project.Commands.Add;

public sealed class AddProjectCommand : IRequest<Result<AddProjectCommandResponse>>
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsArchived { get; set; }
}
