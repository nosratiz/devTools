using System;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Helper;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Account.Auth.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IDevToolsDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IDevToolsDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(
                x => !x.IsDeleted && x.Id == Guid.Parse(_currentUserService.UserId), cancellationToken);

            if (!Utils.CheckPassword(request.CurrentPassword, user.Password))
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.WrongPassword)));

            if (request.CurrentPassword == request.NewPassword)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.PasswordRecentlyUsed)));

            user.Password = Utils.HashPass(request.NewPassword);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.PasswordChangeSuccessfully)));
        }
    }
}