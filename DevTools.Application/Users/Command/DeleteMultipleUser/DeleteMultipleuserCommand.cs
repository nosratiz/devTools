using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Users.Command.DeleteMultipleUser
{
    public class DeleteMultipleUserCommand : IRequest<Result>
    {
        public List<Guid> UserIds { get; set; }
    }

    public class DeleteMultipleUserCommandHandler : IRequestHandler<DeleteMultipleUserCommand, Result>
    {
        private readonly IDevToolsDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteMultipleUserCommandHandler(IDevToolsDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(DeleteMultipleUserCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.UserIds)
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == id, cancellationToken);

                #region Validation

                if (user is null)
                    return Result.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.UserNotFound)));

                if (user.RoleId == Role.Admin)
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage($"User with id: {id} and name: {user.Name} {user.Family}  is Admin")));

                if (_currentUserService.UserId == user.Id.ToString())
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.DeleteYourSelfNotAllowed)));

                user.IsDeleted = true;
                #endregion

            }
            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.DeleteUserSuccessfully)));

        }
    }
}