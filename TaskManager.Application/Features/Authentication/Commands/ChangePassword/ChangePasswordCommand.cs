using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.ChangePassword;

public sealed class ChangePasswordCommand : IRequest<Result<ChangePasswordCommandResponse>>
{
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
