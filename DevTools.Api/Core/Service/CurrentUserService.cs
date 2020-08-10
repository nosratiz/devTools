using System.Security.Claims;
using DevTools.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DevTools.Api.Core.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
            IsAuthenticated = UserId != null;
        }

        public string UserId { get; }
        public bool IsAuthenticated { get; }
    }
}