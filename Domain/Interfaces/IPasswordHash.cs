namespace discagram.Services.Interfaces;

public interface IPasswordHash
{
    string HashPassword(string password);
    bool VerifyPasswordHash(string password,string storedHash);
}