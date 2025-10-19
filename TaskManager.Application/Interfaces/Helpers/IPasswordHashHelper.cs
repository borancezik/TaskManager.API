namespace TaskManager.Application.Interfaces.Helpers;

public interface IPasswordHashHelper
{
    (string Hash, string Salt, int Iterations) CreateHash(string password);
    bool VerifyPassword(string password, string storedHash, string storedSalt, int storedIterations);
}
