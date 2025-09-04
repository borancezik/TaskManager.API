using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Project.Commands.Update;

internal sealed class UpdateProjectCommandHandler(IProjectService projectService) : IRequestHandler<UpdateProjectCommand, Result<UpdateProjectCommandResponse>>
{
    private readonly IProjectService _projectService = projectService;
    public async Task<Result<UpdateProjectCommandResponse>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var projectDto = await _projectService.FindByIdAsync(request.Id);

        if (!projectDto.IsSuccess)
            return Error.NotFound;

        var project = projectDto.Data;

        project.Name = request.Name;
        project.Description = request.Description;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.IsArchived = request.IsArchived;

        var updatedProject = await _projectService.UpdateAsync(project);

        if (!updatedProject.IsSuccess)
            return Error.UpdatedError;

        return new UpdateProjectCommandResponse();
    }
}
