using AuthJwt.Core.Contexts.AccountContext.ValueObjects;

namespace AuthJwt.Tests.UnitTests.Core.ValueObjects;

public class PasswordTest
{
    [Fact]
    public void CreatePasswordShouldNotBeNull()
    {
        var password = new Password();
        Assert.NotNull(password);
    }
    
    [Fact]
    public void VerifyShouldReturnTrueWhenPasswordMatchesHash()
    {
        var password = new Password("senhasecreta");
        Assert.True(Password.Verify("senhasecreta", password.Hash));
    }
}
