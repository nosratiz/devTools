using DevTools.Common.Enum;
using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(e => e.Id).HasColumnName("TicketId").IsRequired();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.Title).IsRequired();

            builder.Property(e => e.Content).IsRequired();

            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.TicketPriority).IsRequired().HasDefaultValue(TicketPriority.NotImportant);

            builder.Property(e => e.TicketStatus).IsRequired().HasDefaultValue(TicketStatus.Open);

            builder.HasOne(e => e.User)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.UserId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}