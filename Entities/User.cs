namespace discagram.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string? Phone { get; set; }
    
    public string UserName { get; set; }
    
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; }  = Array.Empty<byte>();
}