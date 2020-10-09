using DevTools.Application.Users.Command.DeleteMultipleUser;
using FluentValidation;

namespace DevTools.Application.Common.Validator.User
{
    public class DeleteMultipleUserValidator : AbstractValidator<DeleteMultipleUserCommand>
    {
        public DeleteMultipleUserValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(dto=>dto.UserIds).NotNull().NotEmpty().WithMessage("User Id Is Null");
        }
    }
}