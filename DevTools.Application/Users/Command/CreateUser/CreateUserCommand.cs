using DevTools.Application.Users.Model;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Users.Command.CreateUser
{
    public class CreateUserCommand : IRequest<Result<UserDto>>
    {
        public string Name { get; set; }

        public string Family { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public string Mobile { get; set; }
    }
}