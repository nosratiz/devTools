using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Persistence.Context
{
    public class DevToolsDb : DbContext, IDevToolsDb
    {
        public DevToolsDb()
        {

        }

        public DevToolsDb(DbContextOptions<DevToolsDb> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<UserApplication> UserApplications { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<GroupTemplate> GroupTemplates { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<GroupTest> GroupTests { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }

        public Task SaveAsync(CancellationToken cancellationToken) => base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(DevToolsDb).Assembly);

    }
}