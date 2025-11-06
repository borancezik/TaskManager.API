namespace TaskManager.Application.Interfaces.Helpers;

public interface ICurrentUserHelper
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public DateTime ValidTo { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime RefreshTokenEndDate { get; set; }
    public string RefreshToken { get; set; }
}
