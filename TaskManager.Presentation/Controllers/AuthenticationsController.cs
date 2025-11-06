using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Authentication.Commands.ChangePassword;
using TaskManager.Application.Features.Authentication.Commands.Login;
using TaskManager.Application.Features.Authentication.Commands.Register;
using TaskManager.Presentation.Controllers.Base;

namespace TaskManager.Presentation.Controllers;

public class AuthenticationsController(ISender sender) : BaseApiController
{
    private readonly ISender _sender = sender;

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        return await ApiResponse(await _sender.Send(new LoginCommand()));
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        return await ApiResponse(await _sender.Send(request));
    }
}
