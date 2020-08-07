using DevTools.Application.Common.Interfaces;
using DevTools.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevTools.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DevToolsDb>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DevToolsContext")));

            services.AddScoped<IDevToolsDb>(provider => provider.GetService<DevToolsDb>());

            return services;
        }
    }
}