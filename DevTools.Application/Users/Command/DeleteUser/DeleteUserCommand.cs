using System;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Users.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}