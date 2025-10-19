using TaskManager.Application.Utilities.Errors.Base;

namespace TaskManager.Application.Utilities.Errors.ServiceErrors;

public readonly record struct UserErrors
{
    public static readonly Error UsernameOrPasswordIsIncorrect = new("Kullanıcı adı veya şifre hatalı.");

    public static readonly Error NewPasswordIsEqualToCurrentPassword = new("Yeni ve Eski şifreniz aynıdır.");

    public static readonly Error UserNotFound = new("Kullanıcı bulunamadı.");
}
