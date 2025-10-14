using TaskManager.Application.Dtos.Base;

namespace TaskManager.Application.Dtos.User;

public class UserDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; }
    public DateTime JoinedAt { get; set; }
}
