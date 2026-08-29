namespace discagram.Services.Interfaces;

public interface IPasswordHash
{
    (byte[] Hash, byte[] Salt) HashPassword(string password);
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
}