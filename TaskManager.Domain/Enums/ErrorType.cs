using System.ComponentModel;

namespace TaskManager.Domain.Enums;

public enum ErrorType : byte
{
    [Description("Servis Hatası")]
    SERVICE_ERROR = 0,

    [Description("Validasyon Hatası")]
    VALIDATON_ERROR = 1,

    [Description("Bilgilendirme")]
    INFORMATION = 2,

    [Description("Uyarı")]
    WARNING = 3,

    [Description("Dış Servis Hatası")]
    EXTERNAL_SERVICE_ERROR = 4,
}
