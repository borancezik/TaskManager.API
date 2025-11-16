using TaskManager.Application.Utilities.Errors.Base;

namespace TaskManager.Application.Utilities.Errors.ServiceErrors;

public readonly record struct AuthenticationErrors
{
    public static readonly Error InvalidToken = new("Token geçerli değildir.");

    public static readonly Error NotFoundToken = new("Token bulunamadı.");

    public static readonly Error TokenNotCreated = new("Token Oluşturma Hatası.");

    public static readonly Error ExpiredToken = new("Token zaman aşımına uğramıştır.");
}
