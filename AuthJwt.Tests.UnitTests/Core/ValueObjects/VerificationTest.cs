using AuthJwt.Core.Contexts.AccountContext.ValueObjects;

namespace AuthJwt.Tests.UnitTests.Core.ValueObjects;

public class VerificationTest
{
    [Fact]
    public void CreateVerificationShouldInitializeCodeAndExpiration()
    {
        var verification = new Verification();
        
        Assert.NotNull(verification.Code);
    }
    
    [Fact]
    public void VerifyShouldSucceedWhenCodeIsCorrect()
    {
        var verification = new Verification();
        var code = verification.Code;
        verification.Verify(code);

        Assert.True(verification.IsActive);
    }
    
    [Fact]
    public void VerifyShouldThrowExceptionWhenAlreadyVerified()
    {
        var verification = new Verification();
        var code = verification.Code;
        verification.Verify(code);
        
        var exception = Assert.Throws<Exception>(() => verification.Verify(code));

        Assert.Equal("Este item já foi ativado", exception.Message);
    }
    
    [Fact]
    public void VerifyShouldThrowExceptionWhenCodeIsInvalid()
    {
        var verification = new Verification();
        
        var exception = Assert.Throws<Exception>(() => verification.Verify("INVALID"));

        Assert.Equal("Código inválido", exception.Message);
    }
}
