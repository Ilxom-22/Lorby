using Lorby.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lorby.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne<AccessToken>()
            .WithOne(token => token.User)
            .HasForeignKey<AccessToken>(token => token.UserId);

        builder
            .HasMany<VerificationCode>()
            .WithOne(code => code.User)
            .HasForeignKey(code => code.UserId);
    }
}