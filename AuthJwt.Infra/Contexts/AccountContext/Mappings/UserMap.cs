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
            .HasColumnType("VARCHAR(100)")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Image)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .Property(s => s.Address)
            .HasColumnName("Email")
            .IsRequired();

        builder.OwnsOne(x => x.Email)
            .OwnsOne(s => s.Verification)
            .Property(s => s.Code)
            .HasColumnName("EmailVerificationCode")
            .IsRequired();
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne( s => s.Verification)
            .Property(s => s.ExpiresAt)
            .HasColumnName("EmailVerificationExpiresAt")
            .IsRequired();
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne( s => s.Verification)
            .Property(s => s.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt")
            .IsRequired();
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne( s => s.Verification)
            .Ignore(s => s.IsActive);
        
        builder.OwnsOne(x => x.Password)
            .Property(s => s.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();
        
        builder.OwnsOne(x => x.Password)
            .Property(s => s.ResetCode)
            .HasColumnName("PasswordResetCode")
            .IsRequired();
    }
}
