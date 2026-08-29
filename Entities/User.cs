namespace discagram.Entities;

public class User
{
    public Guid Id { get; set; }

    required public string Name { get; set; }

    required public string Email { get; set; }

    required public string UserName { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? Phone { get; set; }

    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    public User() { }

}