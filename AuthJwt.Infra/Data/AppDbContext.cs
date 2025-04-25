using AuthJwt.Core.Contexts.AccountContext.Entities;
using AuthJwt.Infra.Contexts.AccountContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AuthJwt.Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}
