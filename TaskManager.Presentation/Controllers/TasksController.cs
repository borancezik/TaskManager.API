using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Task.Commands.Add;
using TaskManager.Application.Features.Task.Commands.Update;
using TaskManager.Application.Features.Task.Queries.FindById;
using TaskManager.Presentation.Controllers.Base;

namespace TaskManager.Presentation.Controllers;

public class TasksController(ISender sender) : BaseApiController
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(Guid id)
    {
        return await ApiResponse(await _sender.Send(new FindByIdTaskQuery { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddTaskCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTaskCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }
}
