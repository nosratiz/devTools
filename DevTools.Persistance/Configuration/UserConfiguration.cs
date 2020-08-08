using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).HasColumnName("UserId").IsRequired();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email).IsRequired();

            builder.Property(e => e.Name).IsRequired();

            builder.Property(e => e.Family).IsRequired();

            builder.Property(e => e.Password).IsRequired();

            builder.Property(e => e.RegisterDate).IsRequired();

            builder.Property(e => e.IsEmailConfirm).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.IsMobileConfirm).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.Wallet).IsRequired().HasDefaultValue(0);

            builder.Property(e => e.ExpiredDate).IsRequired();

            builder.HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.RoleId)
            .IsRequired();
        }
    }
}