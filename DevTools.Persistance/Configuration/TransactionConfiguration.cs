using DevTools.Common.Enum;
using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasColumnName("TransactionId");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Price).IsRequired();

            builder.Property(e => e.TransactionStatus)
            .IsRequired()
            .HasDefaultValue(TransactionStatus.GoToPortal);

            builder.Property(e => e.CreateDate).IsRequired();

            builder
            .HasOne(e => e.User)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}