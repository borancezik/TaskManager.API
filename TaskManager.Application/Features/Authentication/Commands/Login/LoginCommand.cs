using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Login;

public sealed class LoginCommand : IRequest<Result<LoginCommandResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
