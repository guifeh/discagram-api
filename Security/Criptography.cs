using System.Security.Cryptography;
using discagram.Services.Interfaces;
using Konscious.Security.Cryptography;

namespace discagram.Security;

public class Criptography : IPasswordHash
{
    private const int DegreeOfParallelism = 1;
    private const int MemorySize = 19456;
    private const int Iterations = 2;
    private const int HashLength = 32;
    private const int ArgonVersion = 19;

    public string HashPassword(string password)
    {
        //salt aleatorio de 16 bytes
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        using var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize = MemorySize,
            Iterations = Iterations
        };

        byte[] hash = argon2.GetBytes(HashLength);

        string hashBase64 = Convert.ToBase64String(hash);
        string saltBase64 = Convert.ToBase64String(salt);

        string phc =
            $"$argon2id$v={ArgonVersion}$m={MemorySize},t={Iterations},p={DegreeOfParallelism}${saltBase64}${hashBase64}";

        return phc;
    }
    public bool VerifyPasswordHash(string password, string storedHash)
    {
        string[] partes = storedHash.Split('$');
        
        string paramsPart = partes[3];
        string salt = partes[4];
        string hash = partes[5];

        var dict = new Dictionary<string, int>();
        foreach (var param in paramsPart.Split(','))
        {
            string[] key = param.Split('=');
            dict[key[0]] = int.Parse(key[1]);
        }

        int memorySize = dict["m"];
        int iterations = dict["t"];
        int degreeOfParallelism = dict["p"];
        
        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] hashBytes = Convert.FromBase64String(hash);
        
        using var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password))
        {
            Salt = saltBytes,
            DegreeOfParallelism = degreeOfParallelism,
            MemorySize = memorySize,
            Iterations = iterations
        };

        byte[] newHash = argon2.GetBytes(HashLength);
        
        return CryptographicOperations.FixedTimeEquals(newHash, hashBytes);
    }
}