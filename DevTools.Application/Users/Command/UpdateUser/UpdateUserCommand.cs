using System;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Users.Command.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Email { get; set; }
        
        public int RoleId { get; set; }

        public string Mobile { get; set; }
    }
}