using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Account.Auth.Command.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}