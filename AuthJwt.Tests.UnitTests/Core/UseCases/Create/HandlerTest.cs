using AuthJwt.Core.Contexts.AccountContext.Entities;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Create;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using Moq;

namespace AuthJwt.Tests.UnitTests.Core.UseCases.Create;

public class HandlerTest
{
    private Handler _handler;
    private readonly Mock<IService> _mockService = new();
    private readonly Mock<IRepository> _mockRepository = new();
    
    private readonly Request _invalidRequest;
    private readonly Request _validRequest;
    
    public HandlerTest()
    {
        _handler = new Handler(_mockRepository.Object, _mockService.Object);
        
        _invalidRequest = new Request(new string('a', 165), 
            "email@email.com", "senhasecreta");

        _validRequest = new Request("nome",
            "email@email.com", "senhasecreta");
    }

    [Fact]
    public async Task ShouldThrowErrorWhenRequestIsInvalid()
    {
        var result = await _handler.Handle(_invalidRequest, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("Requisição inválida", result.Message);
    }
    
    [Fact]
    public async Task ShouldSucceedWhenRequestIsValid()
    {
        var result = await _handler.Handle(_validRequest, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal("Conta criada", result.Message);
    }
    
    [Fact]
    public async Task ShouldReturnErrorWhenEmailIsAlreadyInUse()
    {
        _mockRepository.Setup(s 
                => s.AnyAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(true);
        
        _handler = new Handler(_mockRepository.Object, _mockService.Object);
        var result = await _handler.Handle(_validRequest, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("Este e-mail está em uso", result.Message);
    }
    
    [Fact]
    public async Task ShouldReturnErrorWhenSaveUser()
    {
        _mockRepository.Setup(s 
                => s.SaveAsync(It.IsAny<User>(), CancellationToken.None))
            .ThrowsAsync(new Exception());
        
        _handler = new Handler(_mockRepository.Object, _mockService.Object);
        var result = await _handler.Handle(_validRequest, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("Erro ao adicionar conta", result.Message);
    }
}
