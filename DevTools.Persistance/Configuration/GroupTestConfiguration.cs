using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class GroupTestConfiguration : IEntityTypeConfiguration<GroupTest>
    {
        public void Configure(EntityTypeBuilder<GroupTest> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasColumnName("GroupTestId");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.Name).IsRequired();

            builder.HasOne(e => e.User)
            .WithMany(e => e.GroupTest)
            .HasForeignKey(e => e.UserId)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}