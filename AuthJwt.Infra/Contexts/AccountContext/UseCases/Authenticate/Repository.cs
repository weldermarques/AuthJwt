using AuthJwt.Core.Contexts.AccountContext.Entities;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using AuthJwt.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthJwt.Infra.Contexts.AccountContext.UseCases.Authenticate;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) 
        => await context.Users.AsNoTracking().Include(s => s.Roles)
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken: cancellationToken);
}
