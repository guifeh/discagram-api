using System.Security.Cryptography;
using discagram.Services.Interfaces;
using Konscious.Security.Cryptography;

namespace discagram.Security;

public class Criptography : IPasswordHash
{
    private const int DegreeOfParallelism = 8;
    private const int MemorySize = 65536;
    private const int Iterations = 4;
    private const int HashLength = 32;

    public (byte[] Hash, byte[] Salt) HashPassword(string password)
    {
        //salt aleatorio de 16 bytes
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        using var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password))
        {
            Salt =  salt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize =  MemorySize,
            Iterations =  Iterations
        };
        
        byte[] hash = argon2.GetBytes(HashLength);
        
        return (hash, salt);
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password))
        {
            Salt = storedSalt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize = MemorySize,
            Iterations = Iterations
        };
        
        byte[] newHash = argon2.GetBytes(HashLength);
        
        return CryptographicOperations.FixedTimeEquals(newHash, storedHash);
    }
}