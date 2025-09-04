using Mapster;
using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Errors.Base;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Project.Queries.FindById;

internal sealed class FindByIdProjectQueryHandler(IProjectService projectService) : IRequestHandler<FindByIdProjectQuery, Result<FindByIdProjectQueryResponse>>
{
    private readonly IProjectService _projectService = projectService;
    public async Task<Result<FindByIdProjectQueryResponse>> Handle(FindByIdProjectQuery request, CancellationToken cancellationToken)
    {
        var project =  await _projectService.FindByIdAsync(request.Id);

        if (!project.IsSuccess)
            return Error.NotFound;

        return project.Data.Adapt<FindByIdProjectQueryResponse>();
    }
}
