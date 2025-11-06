using MediatR;
using TaskManager.Application.Interfaces.Helpers;
using TaskManager.Application.Interfaces.Services;
using TaskManager.Application.Utilities.Result;

namespace TaskManager.Application.Features.Authentication.Commands.Logout;

internal sealed class LogoutCommandHandler(
    ICurrentUserHelper currentUserHelper,
    ISessionService sessionService) : IRequestHandler<LogoutCommand, Result<LogoutCommandResponse>>
{
    private readonly ICurrentUserHelper _currentUserHelper = currentUserHelper;
    private readonly ISessionService _sessionService = sessionService;
    public async Task<Result<LogoutCommandResponse>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionService.GetByExpressionAsync(x => x.RefreshToken == _currentUserHelper.RefreshToken);

        if (!session.IsSuccess)
            return new LogoutCommandResponse();

        await _sessionService.DeleteByIdAsync(session.Data.Id.Value); 
        
        return new LogoutCommandResponse();
    }
}
