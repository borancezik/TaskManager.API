using TaskManager.Domain.Enums;

namespace TaskManager.Application.Utilities.Errors.Base;

public readonly record struct Error(string Key = null, string Message = null, object[] Args = null, ErrorType Type = ErrorType.SERVICE_ERROR)
{
    /// <summary>
    /// Kayıt bulunamadı.
    /// </summary>
    public static readonly Error NotFound = new("GeneralError001");

    /// <summary>
    /// Aradığınız kriterlere uygun kayıt bulunamadı.
    /// </summary>
    public static readonly Error NotFoundByQuery = new("GeneralError002");

    /// <summary>
    /// Kayıt eklenemedi.
    /// </summary>
    public static readonly Error AddedError = new("GeneralError003");

    /// <summary>
    /// Kayıt silinirken hata oluştu.
    /// </summary>
    public static readonly Error DeletedError = new("GeneralError004");

    /// <summary>
    /// Kayıt güncellenemedi.
    /// </summary>
    public static readonly Error UpdatedError = new("GeneralError005");

    /// <summary>
    /// EntityName veya Id bulunamadı.
    /// </summary>
    public static readonly Error EntityNameOrIdNotFound = new("GeneralError006");

    public static Error CreateErrorOnlyMessage(string message)
    {
        return new(Message: message);
    }

    public static Error CreateErrorMessageWithKey(string message, string key)
    {
        return new(Message: message, Key: key);
    }
}
