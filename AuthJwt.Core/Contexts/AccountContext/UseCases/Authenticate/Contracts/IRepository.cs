using AuthJwt.Core.Contexts.AccountContext.Entities;

namespace AuthJwt.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}
