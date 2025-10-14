using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.User.Commands.Update;

public sealed class UpdateUserCommand : IRequest<Result<UpdateUserCommandResponse>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
