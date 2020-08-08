using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class GroupTemplateConfiguration : IEntityTypeConfiguration<GroupTemplate>
    {
        public void Configure(EntityTypeBuilder<GroupTemplate> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasColumnName("GroupTemplateId");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.Name).IsRequired();

            builder.HasOne(e => e.User)
            .WithMany(e => e.GroupTemplates)
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}