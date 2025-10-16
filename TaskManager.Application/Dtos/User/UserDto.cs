using TaskManager.Application.Dtos.Base;

namespace TaskManager.Application.Dtos.User;

public class UserDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public int Iterations { get; set; }
    public DateTime JoinedAt { get; set; }
}
