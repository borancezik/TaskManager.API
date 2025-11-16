namespace TaskManager.Application.Utilities.Authorization.Model;

public class TokenModel
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime ValidTo { get; set; }
    public DateTime RefreshTokenEndDate { get; set; }
    public string RefreshToken { get; set; }
}
