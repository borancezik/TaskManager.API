using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Project.Commands.Add;
using TaskManager.Application.Features.Project.Commands.Update;
using TaskManager.Application.Features.Project.Queries.FindById;
using TaskManager.Presentation.Controllers.Base;

namespace TaskManager.Presentation.Controllers;

public class ProjectsController(ISender sender) : BaseApiController
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(Guid id)
    {
        return await ApiResponse(await _sender.Send(new FindByIdProjectQuery { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProjectCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProjectCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }
}
