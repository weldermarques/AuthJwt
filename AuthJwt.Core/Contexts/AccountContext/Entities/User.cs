using AuthJwt.Core.Contexts.AccountContext.ValueObjects;
using AuthJwt.Core.Contexts.SharedContext.Entities;

namespace AuthJwt.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    protected User()
    {
        
    }
    
    public User(string name, string email, string? password = null)
    {
        Name = name;
        Email = email;
        Password = new Password(password);
    }
    
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; }
    public Password Password { get; private set; } = null!;
    public string Image { get; private set; } = string.Empty;

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if(!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de restauração inválido");
        
        Password = new Password(plainTextPassword);
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public void ChangePassword(string plainTextPassword)
    {
        Password = new Password(plainTextPassword);
    }
}
