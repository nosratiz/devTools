using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Account.Auth.Command.ForgetPassword
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Result>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IEmailServices _emailServices;

        public ForgetPasswordCommandHandler(IDevToolsDbContext context, IEmailServices emailServices)
        {
            _context = context;
            _emailServices = emailServices;
        }

        public async Task<Result> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => !x.IsDeleted && x.Email == request.Email, cancellationToken);

            if (user is null)
                return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.ForgetPasswordSentSuccessfully)));

            await _emailServices.SendMessage(request.Email, "Forgot Password", "your html").ConfigureAwait(false);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.ForgetPasswordSentSuccessfully)));
        }
    }
}