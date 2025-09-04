namespace TaskManager.Application.Utilities.Errors.ValidationErrors;

public static class TaskValidationError
{
    public const string IdRequired = "Id boş olamaz.";
    public const string ProjectIdRequired = "ProjectId boş olamaz.";
    public const string TitleRequired = "Title boş olamaz.";
    public const string TitleTooLong = "Title en fazla 200 karakter olabilir.";
    public const string DescriptionTooLong = "Description en fazla 1000 karakter olabilir.";
    public const string StatusRequired = "Status boş olamaz.";
    public const string DueDateInvalid = "DueDate geçmiş bir tarih olamaz.";
    public const string PriorityOutOfRange = "Priority 1 ile 5 arasında olmalıdır.";
}
