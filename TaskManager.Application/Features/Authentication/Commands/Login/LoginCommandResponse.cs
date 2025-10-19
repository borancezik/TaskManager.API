namespace TaskManager.Application.Features.Authentication.Commands.Login;

public sealed class LoginCommandResponse
{
    public string AccessToken { get; set; } = null!;
}
