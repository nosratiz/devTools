using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Account.Auth.ModelDto;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using Microsoft.AspNetCore.Authentication;

namespace DevTools.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        Task<Result<AuthenticationResult>> Generate(User user,CancellationToken cancellationToken);
    }
}