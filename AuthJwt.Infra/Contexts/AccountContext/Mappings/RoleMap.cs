using AuthJwt.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthJwt.Infra.Contexts.AccountContext.Mappings;

public class RoleMap : IEntityTypeConfiguration<Role>
{

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        
        builder
            .Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
