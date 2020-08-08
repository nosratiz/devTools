using DevTools.Common.Enum;
using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasColumnName("TestId");

            builder.Property(e => e.Name).IsRequired();

            builder.Property(e => e.ContentType).IsRequired().HasDefaultValue(ContentType.ApplicationJson);

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.StatusCodeExpect).IsRequired();

            builder.Property(e => e.HttpMethod).IsRequired();

            builder.HasOne(e => e.GroupTest)
                .WithMany(e => e.Tests)
                .HasForeignKey(e => e.GroupTestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}