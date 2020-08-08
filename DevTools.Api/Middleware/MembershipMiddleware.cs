using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using DevTools.Common.Extensions;
using DevTools.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DevTools.Api.Middleware
{
    public class MembershipMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _options;

        public MembershipMiddleware(RequestDelegate next,
            IOptions<JwtSettings> options)
        {
            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.HasAuthorization())
            {
                var token = httpContext.GetAuthorizationToken();
                var secretKey = Encoding.UTF8.GetBytes(_options.Secret);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateAudience = true, //default : false
                    ValidAudience = _options.ValidAudience,
                    ValidateIssuer = true, //default : false
                    ValidIssuer = _options.ValidIssuer,

                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                };

                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var claimsPrincipal = handler.ValidateToken(token, validationParameters, out _);
                    httpContext.User = claimsPrincipal;
                }
                catch (SecurityTokenExpiredException)
                {
                    await httpContext.WriteError("Token was expired.", StatusCodes.Status401Unauthorized);
                }
                catch (SecurityTokenNoExpirationException)
                {
                    await httpContext.WriteError("Invalid token data. [NoExpiration]",
                        StatusCodes.Status401Unauthorized);
                }
                catch (SecurityTokenNotYetValidException)
                {
                    await httpContext.WriteError("Security token not yet valid.", StatusCodes.Status401Unauthorized);
                }
                catch (SecurityTokenInvalidAudienceException)
                {
                    await httpContext.WriteError("Invalid audience", StatusCodes.Status401Unauthorized);
                }
                catch (SecurityTokenInvalidIssuerException)
                {
                    await httpContext.WriteError("Invalid issuer.", StatusCodes.Status401Unauthorized);
                }
                catch (SecurityTokenInvalidSignatureException)
                {
                    await httpContext.WriteError("Invalid signature.", StatusCodes.Status401Unauthorized);
                }
                catch (SecurityTokenInvalidSigningKeyException)
                {
                    await httpContext.WriteError("Invalid signing key.", StatusCodes.Status401Unauthorized);
                }
                catch (Exception ex)
                {
                    await httpContext.WriteError(ex.Message, StatusCodes.Status401Unauthorized);
                }
            }

            await _next(httpContext);
        }
    }
}