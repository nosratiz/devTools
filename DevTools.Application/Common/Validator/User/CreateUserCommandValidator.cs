using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Users.Command.CreateUser;
using DevTools.Common.General;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Common.Validator.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IDevToolsDbContext _context;

        public CreateUserCommandValidator(IDevToolsDbContext context)
        {
            _context = context;

            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();

            RuleFor(dto => dto.Name).NotEmpty().MinimumLength(3);

            RuleFor(dto => dto.Family).NotEmpty().MinimumLength(3);

            RuleFor(dto => dto.Password).NotEmpty().MinimumLength(6);

            RuleFor(dto => dto)
                .MustAsync(ValidEmailAddress).WithMessage(ResponseMessage.EmailAlreadyExist)
                .MustAsync(ValidMobile).WithMessage(ResponseMessage.MobileAlreadyExist)
                .MustAsync(ValidRole).WithMessage(ResponseMessage.RoleNotFound);
        }

        private async Task<bool> ValidEmailAddress(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(x => x.IsDeleted == false && x.Email == createUserCommand.Email, cancellationToken);
        }


        private async Task<bool> ValidMobile(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(x => x.IsDeleted == false && x.Mobile == createUserCommand.Mobile, cancellationToken);
        }

        private async Task<bool> ValidRole(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(x => x.Id == createUserCommand.RoleId, cancellationToken);
        }




    }
}