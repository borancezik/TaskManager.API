using System.Security.Cryptography;
using System.Text;
using TaskManager.Application.Interfaces.Helpers;

namespace TaskManager.Infrastructure.Helpers;

public class PasswordHashHelper : IPasswordHashHelper
{
    private const int SaltSize = 16; // 128 bit salt
    private const int HashSize = 32; // 256 bit SHA3_256 output

    public (string Hash, string Salt) CreateHash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        // 1️⃣ Rastgele salt üret
        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

        // 2️⃣ Hash hesapla (password + salt)
        var hashBytes = ComputeSha3Hash(password, saltBytes);

        // 3️⃣ String olarak döndür
        var hashString = Convert.ToHexString(hashBytes);
        var saltString = Convert.ToHexString(saltBytes);

        return (hashString, saltString);
    }

    public bool Verify(string password, string storedHash, string storedSalt)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        var saltBytes = Convert.FromHexString(storedSalt);
        var computedHash = ComputeSha3Hash(password, saltBytes);
        var storedHashBytes = Convert.FromHexString(storedHash);

        // Sabit zamanlı karşılaştırma (timing attack koruması)
        return CryptographicOperations.FixedTimeEquals(computedHash, storedHashBytes);
    }

    private static byte[] ComputeSha3Hash(string password, byte[] salt)
    {
        using var sha3 = SHA3_256.Create();

        var combinedBytes = new byte[Encoding.UTF8.GetByteCount(password) + salt.Length];
        Encoding.UTF8.GetBytes(password, 0, password.Length, combinedBytes, 0);
        Buffer.BlockCopy(salt, 0, combinedBytes, Encoding.UTF8.GetByteCount(password), salt.Length);

        return sha3.ComputeHash(combinedBytes);
    }
}
