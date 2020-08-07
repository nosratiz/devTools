using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTools.Persistence.Configuration
{
    public class GroupTemplateConfiguration : IEntityTypeConfiguration<GroupTemplate>
    {
        public void Configure(EntityTypeBuilder<GroupTemplate> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}