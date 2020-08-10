using System.Threading.Tasks;
using DevTools.Application.Account.Auth.Command;
using DevTools.Application.Account.Auth.ModelDto;
using DevTools.Common.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Common
{

    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// login to Panel
        /// </summary>
        /// <param name="loginCommand"></param>
        /// <returns>Access and refresh token.</returns>
        /// <response code="200">if login successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);

            return result.ApiResult;
        }


    }
}
