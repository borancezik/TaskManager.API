namespace TaskManager.Application.Interfaces.Helpers;

public interface ITokenHelper
{
    string GenerateToken(Guid userId, string username, string email);
}
