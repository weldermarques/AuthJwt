using AuthJwt.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthJwt.Infra.Contexts.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Image)
            .HasMaxLength(255)
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .Property(s => s.Address)
            .HasColumnName("Email")
            .HasMaxLength(255)
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .OwnsOne(s => s.Verification)
            .Property(s => s.Code)
            .HasMaxLength(6)
            .HasColumnName("EmailVerificationCode")
            .IsRequired();
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne( s => s.Verification)
            .Property(s => s.ExpiresAt)
            .HasColumnName("EmailVerificationExpiresAt");
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne( s => s.Verification)
            .Property(s => s.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt");
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne( s => s.Verification)
            .Ignore(s => s.IsActive);
        
        builder.OwnsOne(x => x.Password)
            .Property(s => s.Hash)
            .HasMaxLength(100)
            .HasColumnName("PasswordHash")
            .IsRequired();
        
        builder.OwnsOne(x => x.Password)
            .Property(s => s.ResetCode)
            .HasMaxLength(8)
            .HasColumnName("PasswordResetCode")
            .IsRequired();

        builder.HasMany(s => s.Roles)
            .WithMany(s => s.Users);
    }
}
