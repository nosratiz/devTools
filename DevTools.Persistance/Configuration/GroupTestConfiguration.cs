using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class GroupTestConfiguration : IEntityTypeConfiguration<GroupTest>
    {
        public void Configure(EntityTypeBuilder<GroupTest> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}