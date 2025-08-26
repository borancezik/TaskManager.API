namespace TaskManager.Application.Utilities.Errors.ValidationErrors;

public static class ProjectValidationError
{
    public const string ProjectNameRequired = "Proje adı zorunludur.";
    public const string ProjectNameTooLong = "Proje adı en fazla 100 karakter olabilir.";
    public const string StartDateRequired = "Başlangıç tarihi zorunludur.";
    public const string EndDateGreaterThanStartDate = "Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.";
}
