using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.User.Commands.Update;

public sealed class UpdateUserCommand : IRequest<Result<UpdateUserCommandResponse>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    public int Iterations { get; set; } 
    public DateTime JoinedAt { get; set; }
}
