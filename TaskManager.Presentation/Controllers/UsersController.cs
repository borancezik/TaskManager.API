using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.User.Commands.Add;
using TaskManager.Application.Features.User.Commands.Update;
using TaskManager.Application.Features.User.Queries.FindById;
using TaskManager.Presentation.Controllers.Base;

namespace TaskManager.Presentation.Controllers;

public class UsersController(ISender sender) : BaseApiController
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(Guid id)
    {
        return await ApiResponse(await _sender.Send(new FindByIdUserQuery { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddUserCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }
}
