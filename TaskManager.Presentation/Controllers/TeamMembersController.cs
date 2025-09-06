using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.TeamMember.Commands.Add;
using TaskManager.Application.Features.TeamMember.Commands.Update;
using TaskManager.Application.Features.TeamMember.Queries.FindById;
using TaskManager.Presentation.Controllers.Base;

namespace TaskManager.Presentation.Controllers;

public class TeamMembersController(ISender sender) : BaseApiController
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(Guid id)
    {
        return await ApiResponse(await _sender.Send(new FindByIdTeamMemberQuery { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddTeamMemberCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTeamMemberCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }
}
