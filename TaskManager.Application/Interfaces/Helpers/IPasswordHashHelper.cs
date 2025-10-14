namespace TaskManager.Application.Interfaces.Helpers;

public interface IPasswordHashHelper
{
    (string Hash, string Salt) CreateHash(string password);
    bool Verify(string password, string storedHash, string storedSalt);
}
