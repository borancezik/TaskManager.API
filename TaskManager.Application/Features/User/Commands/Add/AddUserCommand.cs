using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.User.Commands.Add;

public sealed class AddUserCommand : IRequest<Result<AddUserCommandResponse>>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
