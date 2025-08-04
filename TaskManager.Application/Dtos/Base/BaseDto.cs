namespace TaskManager.Application.Dtos.Base;

public class BaseDto
{
    public long? Id { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
