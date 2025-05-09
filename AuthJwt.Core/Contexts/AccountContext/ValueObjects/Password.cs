﻿using AuthJwt.Core.Contexts.SharedContext.ValueObjects;
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
    
    public bool Challenge(string plainTextPassword)
        => Verify(plainTextPassword, Hash);
    
    public string Hash { get; } = string.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();
    
    private static string Generate()
        => PasswordGenerator.Generate();
    private static string Hashing(string plainTextPassword)
        => PasswordHasher.Hash(plainTextPassword, privateKey: Configuration.Secrets.PasswordSaltKey);
    private static bool Verify(string plainTextPassword, string hashPassword)
        => PasswordHasher.Verify(hashPassword, plainTextPassword, privateKey: Configuration.Secrets.PasswordSaltKey);
}
