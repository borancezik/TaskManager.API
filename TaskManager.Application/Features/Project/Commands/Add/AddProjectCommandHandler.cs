using MediatR;
using TaskManager.Application.Dtos.Project;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Project.Commands.Add;

internal sealed class AddProjectCommandHandler(IProjectService projectService) : IRequestHandler<AddProjectCommand, Result<AddProjectCommandResponse>>
{
    private readonly IProjectService _projectService = projectService;
    public async Task<Result<AddProjectCommandResponse>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var projectDto = new ProjectDto
        {
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsArchived = request.IsArchived
        };

        var addedProject = await _projectService.AddAsync(projectDto);

        if (!addedProject.IsSuccess)
        {
            return Error.AddedError;
        }

        return new AddProjectCommandResponse
        {
            Id = addedProject.Data.Id,
        };
    }
}
