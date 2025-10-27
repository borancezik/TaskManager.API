namespace TaskManager.Application.Utilities.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Auth Error!") { }
    public UnauthorizedException(string message) : base(message) { }
}
