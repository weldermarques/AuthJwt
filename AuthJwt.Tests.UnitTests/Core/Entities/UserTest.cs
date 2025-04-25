using AuthJwt.Core.Contexts.AccountContext.Entities;

namespace AuthJwt.Tests.UnitTests.Core.Entities;

public class UserTest
{
    [Fact]
    public void ShouldReturnErrorWhenTryUpdatingPasswordWithInvalidCode()
    {
        var user = new User("Nome Proprio", "email@email.com", "senhasecreta");

        var exception = Assert.Throws<Exception>(() =>
        {
            user.UpdatePassword("novasenhasecreta", "ADA4T");
        });
        
        Assert.Equal("Código de restauração inválido", exception.Message);
    }
    
    [Fact]
    public void ShouldUpdatePasswordWhenCodeIsValid()
    {
        var user = new User("Nome Proprio", "email@email.com", "senhasecreta");
        var code = user.Password.ResetCode;
        
        user.UpdatePassword("novasenhasecreta", code.ToLower());
    }
}
