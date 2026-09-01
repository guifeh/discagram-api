namespace discagram.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    required public string Name { get; set; }

    required public string Email { get; set; }

    required public string UserName { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? Phone { get; set; }

    required public string PasswordHash { get; set; } 

    Index UserIndex { get; set; }
    Index EmailIndex { get; set; }
    Index UserNameIndex { get; set; }
    public User() { }

}