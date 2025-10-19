using System.Security.Cryptography;
using System.Text;
using TaskManager.Application.Interfaces.Helpers;

namespace TaskManager.Infrastructure.Helpers;

public class PasswordHashHelper : IPasswordHashHelper
{
    private const int SaltSize = 16;   // 128 bit salt
    private const int HashSize = 32;   // 256 bit SHA3_256 output
    private const int MinIterations = 50_000;
    private const int MaxIterations = 150_000; // inclusive upper bound handled below

    /// <summary>
    /// Yeni salt üretir, rastgele iterations seçer (50k..150k) ve hash oluşturur.
    /// Dönen tuple: (HashHex, SaltHex, Iterations)
    /// </summary>
    public (string Hash, string Salt, int Iterations) CreateHash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

        int iterations = RandomNumberGenerator.GetInt32(MinIterations, MaxIterations + 1);

        var hashBytes = ComputeSha3Hash(password, saltBytes, iterations);

        return (
            Hash: Convert.ToHexString(hashBytes),
            Salt: Convert.ToHexString(saltBytes),
            Iterations: iterations
        );
    }

    /// <summary>
    /// Verilen password ile stored hash'i iterations kullanarak doğrular.
    /// </summary>
    public bool VerifyPassword(string password, string storedHash, string storedSalt, int storedIterations)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        var saltBytes = Convert.FromHexString(storedSalt);
        var computedHash = ComputeSha3Hash(password, saltBytes, storedIterations);
        var storedHashBytes = Convert.FromHexString(storedHash);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHashBytes);
    }

    private static byte[] ComputeSha3Hash(string password, byte[] salt, int iterations)
    {
        using var sha3 = SHA3_256.Create();

        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var combined = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, combined, passwordBytes.Length, salt.Length);

        var hash = sha3.ComputeHash(combined);

        for (int i = 1; i < iterations; i++)
        {
            hash = sha3.ComputeHash(hash);
        }

        return hash;
    }
}
