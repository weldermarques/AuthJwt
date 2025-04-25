using AuthJwt.Core.Contexts.AccountContext.Entities;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}
