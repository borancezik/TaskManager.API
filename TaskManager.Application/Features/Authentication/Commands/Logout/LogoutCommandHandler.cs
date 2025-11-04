using MediatR;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Authorization.Session;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Logout;

internal sealed class LogoutCommandHandler(
    SessionManager sessionManager,
    ISessionService sessionService) : IRequestHandler<LogoutCommand, Result<LogoutCommandResponse>>
{
    private readonly SessionManager _sessionManager = sessionManager;
    private readonly ISessionService _sessionService = sessionService;
    public async Task<Result<LogoutCommandResponse>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionService.GetByExpressionAsync(x => x.RefreshToken == _sessionManager.LoginResult.RefreshToken);

        if (!session.IsSuccess)
            return new LogoutCommandResponse();

        await _sessionService.DeleteByIdAsync(session.Data.Id.Value); 
        
        return new LogoutCommandResponse();
    }
}
