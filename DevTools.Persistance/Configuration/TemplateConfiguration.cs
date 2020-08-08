using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasColumnName("TemplateId");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired();

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.Content).IsRequired();

            builder.HasOne(e => e.GroupTemplate)
                .WithMany(e => e.Templates)
                .HasForeignKey(e => e.GroupTemplateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}