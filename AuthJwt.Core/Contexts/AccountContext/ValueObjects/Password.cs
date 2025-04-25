using AuthJwt.Core.Contexts.SharedContext.ValueObjects;
using SecureIdentity.Password;

namespace AuthJwt.Core.Contexts.AccountContext.ValueObjects;

public class Password : ValueObject
{
    protected Password()
    {
        
    }

    public Password(string? plainTextPassword = null)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            plainTextPassword = Generate();

        Hash = Hashing(plainTextPassword);
    }
    public string Hash { get; } = string.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

    private static string Generate()
        => PasswordGenerator.Generate();
    private static string Hashing(string plainTextPassword)
        => PasswordHasher.Hash(plainTextPassword, privateKey: Configuration.Secrets.PasswordSaltKey);
    public static bool Verify(string plainTextPassword, string hashPassword)
        => PasswordHasher.Verify(hashPassword, plainTextPassword);
}
