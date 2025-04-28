using MediatR;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Authenticate;

public record Request(
    string Email, 
    string Password) : IRequest<Response>;
