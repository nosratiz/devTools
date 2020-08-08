using Microsoft.AspNetCore.Builder;

namespace DevTools.Api.Middleware
{
    public static class AccessControlAllowOriginAlwaysExtensions
    {
        public static IApplicationBuilder UseAccessControlAllowOriginAlways(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessControlAllowOriginAlways>();
        }
    }
}