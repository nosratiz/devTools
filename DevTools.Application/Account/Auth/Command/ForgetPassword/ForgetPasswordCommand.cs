using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Account.Auth.Command.ForgetPassword
{
    public class ForgetPasswordCommand : IRequest<Result>
    {
        public string Email { get; set; }
    }
}