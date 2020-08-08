using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasColumnName("SettingId");

            builder.Property(e => e.Title).IsRequired();

            builder.Property(e => e.CopyRight).IsRequired();

            builder.Property(e => e.Logo).IsRequired();
        }
    }
}