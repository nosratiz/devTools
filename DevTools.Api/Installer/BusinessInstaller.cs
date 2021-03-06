﻿using DevTools.Api.Core.Service;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Common.Service;
using DevTools.Common.Helper;
using DevTools.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevTools.Api.Installer
{
    public class BusinessInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<IRequestMeta, RequestMeta>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();

            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.Configure<EmailSetting>(configuration.GetSection("EmailSetting"));
        }
    }
}