namespace NT.Domain.Users;
using NT.Domain.Common;
using Crypt = BCrypt.Net.BCrypt;

public class User : Entity
{
    public string Login { get; private set; }

    public string PasswordHash { get; private set; }

    public Role Role { get; private set; }

    private User(string login, string passwordHash, Role role)
    {
        Login = login;
        PasswordHash = passwordHash;
        Role = role;
    }

    private User() { }

    public static User Empty() => new User();
    
    public static User Create(string login, string password)
    {
        var hash = Crypt.EnhancedHashPassword(password, 13);
        return new User(login, hash, Role.Default);
    }

    public bool Verify(string password) => Crypt.EnhancedVerify(password, PasswordHash);
}
