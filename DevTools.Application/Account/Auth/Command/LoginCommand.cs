using DevTools.Application.Account.Auth.ModelDto;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Account.Auth.Command
{
    public class LoginCommand : IRequest<Result<TokenDto>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}