using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Account.Auth.ModelDto;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.Helper;
using DevTools.Common.Options;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DevTools.Application.Common.Service
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDevToolsDbContext _context;
        private readonly IRequestMeta _requestMeta;

        public TokenGenerator(IOptions<JwtSettings> jwtSettings, IRequestMeta requestMeta, IDevToolsDbContext context)
        {
            _requestMeta = requestMeta;
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result<AuthenticationResult>> Generate(User user, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),

            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(14),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtSettings.ValidAudience,
                Issuer = _jwtSettings.ValidIssuer
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToken = new UserToken
            {
                Token = tokenHandler.WriteToken(token),
                UserId = user.Id,
                CreateDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddDays(14),
                Browser = _requestMeta.Browser,
                Os = _requestMeta.Os,
                Ip = _requestMeta.Ip,
                UserAgent = _requestMeta.UserAgent,
            };

            await _context.UserTokens.AddAsync(userToken, cancellationToken);

            await _context.SaveAsync(cancellationToken);


            return Result<AuthenticationResult>.SuccessFul(new AuthenticationResult
            {
                AccessToken = userToken.Token,
                IsSuccess = true,
                RoleId = user.RoleId
            });
        }
    }
}