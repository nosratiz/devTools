using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Users.Command.UpdateUser;
using DevTools.Common.General;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Common.Validator.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IDevToolsDbContext _context;

        public UpdateUserCommandValidator(IDevToolsDbContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.Stop;

            RuleFor(dto => dto.Id).NotEmpty().NotNull();

            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();

            RuleFor(dto => dto.Name).NotEmpty().MinimumLength(3);

            RuleFor(dto => dto.Family).NotEmpty().MinimumLength(3);

            RuleFor(dto => dto)
                .MustAsync(ValidEmailAddress).WithMessage(ResponseMessage.EmailAlreadyExist)
                .MustAsync(ValidMobile).WithMessage(ResponseMessage.MobileAlreadyExist)
                .MustAsync(ValidRole).WithMessage(ResponseMessage.RoleNotFound);
        }

        private async Task<bool> ValidEmailAddress(UpdateUserCommand updateUserCommand,
            CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(x => x.IsDeleted == false
                                                       && x.Id != updateUserCommand.Id
                                                       && x.Email == updateUserCommand.Email, cancellationToken);
        }


        private async Task<bool> ValidMobile(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(x => x.IsDeleted == false
                                                       && x.Id != updateUserCommand.Id
                                                       && x.Mobile == updateUserCommand.Mobile, cancellationToken);
        }

        private async Task<bool> ValidRole(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(x => x.Id == updateUserCommand.RoleId, cancellationToken);
        }
    }
}