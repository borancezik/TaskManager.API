using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Register;

public sealed class RegisterCommand : IRequest<Result<RegisterCommandResponse>>
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
