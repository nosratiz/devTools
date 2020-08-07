using System.Threading;
using System.Threading.Tasks;
using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Common.Interfaces
{
    public interface IDevToolsDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
        DbSet<UserApplication> UserApplications { get; set; }
        DbSet<Template> Templates { get; set; }
        DbSet<GroupTemplate> GroupTemplates { get; set; }
        DbSet<Test> Tests { get; set; }
        DbSet<GroupTest> GroupTests { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<Setting> Settings { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
    }
}