using MediatR;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Logout;

public sealed class LogoutCommand : IRequest<Result<LogoutCommandResponse>>;
