using TaskManager.Application.Utilities.Errors.Base;

namespace TaskManager.Application.Utilities.Result;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public List<Error> Errors { get; }

    public Result(Error? error = null, List<Error>? errors = null)
    {
        IsSuccess = error is null && (errors == null || !errors.Any());
        Error = error;
        Errors = errors ?? null;
    }

    public static implicit operator Result(Error error) => new(error: error);
    public static implicit operator Result(List<Error> errors) => new(errors: errors);
}
