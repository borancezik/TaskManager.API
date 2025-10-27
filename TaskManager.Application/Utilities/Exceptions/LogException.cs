namespace TaskManager.Application.Utilities.Exceptions;

public class LogException : Exception
{
    public LogException() : base("Log Error!") { }
    public LogException(string message) : base(message) { }
}
