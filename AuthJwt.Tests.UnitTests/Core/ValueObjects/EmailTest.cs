using AuthJwt.Core.AccountContext.ValueObjects;
using Xunit.Sdk;

namespace AuthJwt.Tests.UnitTests.Core.ValueObjects;

public class EmailTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("min")]
    [InlineData("invalid@@email.com")]
    public void ShouldThrowExceptionWithInvalidEmail(string address)
    {
        var exception = Assert.Throws<Exception>(() =>
        {
            Email email = address;
        });
        
        Assert.Contains("Email inválido", exception.Message);
    }
    
    [Fact]
    public void ShouldCreatedEmailWithValidAddress()
    {
        Email email = "valid@email.com";
        
        Assert.NotNull(email);
        Assert.Equal("valid@email.com", email);
    }
}
