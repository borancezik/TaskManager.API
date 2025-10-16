namespace TaskManager.Application.Utilities.Errors.ValidationErrors;

public static class UserValidationError
{
    public const string IdRequired = "Id is not empty.";
    public const string NameRequired = "Name is required.";
    public const string NameTooLong = "Name must not exceed 100 characters.";
    public const string UserNameRequired = "UserName is required.";
    public const string UserNameTooLong = "UserName must not exceed 100 characters.";
    public const string EmailRequired = "Email is required.";
    public const string EmailInvalid = "Email format is invalid.";
    public const string EmailTooLong = "Email must not exceed 150 characters.";
    public const string JoinedAtRequired = "Joined date is required.";
    public const string JoinedAtInFuture = "Joined date cannot be in the future.";
}
