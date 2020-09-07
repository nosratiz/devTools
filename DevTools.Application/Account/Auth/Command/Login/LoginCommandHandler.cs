using System;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Account.Auth.ModelDto;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Helper;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Account.Auth.Command
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly ITokenGenerator _tokenGenerator;


        public LoginCommandHandler(IDevToolsDbContext context, ITokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x =>
                    x.IsDeleted == false && x.Email == request.Email.ToLower(), cancellationToken);

            #region Validate Account

            if (user is null)
                return Result<TokenDto>.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.InvalidCredential)));

            if (!Utils.CheckPassword(request.Password, user.Password))
                return Result<TokenDto>.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.InvalidCredential)));

            if (user.IsEmailConfirm == false)
                return Result<TokenDto>.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.AccountNotActive)));


            #endregion

            var userToken = await _context.UserTokens
                .FirstOrDefaultAsync(x => x.IsExpired == false
                                          && x.UserId == user.Id && x.ExpireDate.Date >= DateTime.Today, cancellationToken);

            //if user already have valid token in database
            if (userToken != null)
            {
                return Result<TokenDto>.SuccessFul(new TokenDto
                {
                    AccessToken = userToken.Token,
                    RoleId = user.RoleId
                });
            }

            var result = await _tokenGenerator.Generate(user, cancellationToken);


            return Result<TokenDto>.SuccessFul(new TokenDto
            {
                AccessToken = result.Data.AccessToken,
                RoleId = user.RoleId
            });
        }
    }
}