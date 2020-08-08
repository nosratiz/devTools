using DevTools.Common.Enum;
using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.Property(e => e.Id).HasColumnName("UserTokenId").IsRequired();

            builder.Property(e => e.Token).IsRequired();

            builder.Property(e => e.UserAgent).IsRequired();

            builder.Property(e => e.TokenType).IsRequired().HasDefaultValue(TokenType.Application);

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.ExpireDate).IsRequired();

            builder.Property(e => e.Ip).IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(e => e.UserTokens)
                .HasForeignKey(e => e.UserId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}