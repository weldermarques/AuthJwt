using AuthJwt.Core.Contexts.AccountContext.Entities;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using AuthJwt.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthJwt.Infra.Contexts.AccountContext.UseCases.Create;

public class Repository(AppDbContext context) : IRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken) 
        => await _context.Users.AsNoTracking().AnyAsync(s => s.Email.Address == email, cancellationToken);

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
