using DevTools.Api.Installer;
using DevTools.Api.Middleware;
using DevTools.Application;
using DevTools.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DevTools.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesAssembly(Configuration);

            services.AddApplication();

            services.AddPersistence(Configuration);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");


            app.UseAccessControlAllowOriginAlways();

            app.UseMiddleware<ApplicationMetaMiddleware>();
            app.UseMiddleware<MembershipMiddleware>();

            app.UseAuthorization();

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hang-fire");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dev Tools  API V1"); });

        }
    }
}
