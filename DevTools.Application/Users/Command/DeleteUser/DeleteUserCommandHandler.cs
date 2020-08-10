using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Users.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IDevToolsDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteUserCommandHandler(IDevToolsDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id, cancellationToken);

            #region Validation

            if (user is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

            if (user.RoleId == Role.Admin)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.AdminDeleteNotAllowed)));

            if (_currentUserService.UserId == user.Id.ToString())
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.DeleteYourSelfNotAllowed)));


            #endregion

            user.IsDeleted = true;
            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.DeleteUserSuccessfully)));
        }
    }
}