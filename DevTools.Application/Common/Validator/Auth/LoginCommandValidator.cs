using DevTools.Application.Account.Auth.Command;
using FluentValidation;

namespace DevTools.Application.Common.Validator.Auth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(dto => dto.Email).NotEmpty().EmailAddress();

            RuleFor(dto => dto.Password).NotEmpty();
        }
    }
}