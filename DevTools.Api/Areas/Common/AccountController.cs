using System.Threading.Tasks;
using DevTools.Application.Account.Auth.Command;
using DevTools.Application.Account.Auth.Command.ChangePassword;
using DevTools.Application.Account.Auth.Command.ForgetPassword;
using DevTools.Application.Account.Auth.ModelDto;
using DevTools.Application.Users.Model;
using DevTools.Application.Users.Queries;
using DevTools.Common.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Forget Password
        /// </summary>
        /// <param name="forgetPasswordCommand"></param>
        /// <returns>Send Email For Forget Password</returns>
        /// <response code="200">if forget Password send successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordCommand forgetPasswordCommand)
        {
            var result = await _mediator.Send(forgetPasswordCommand);

            return result.ApiResult;
        }


        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="changePasswordCommand"></param>
        /// <returns>To Change Your Current Password</returns>
        /// <response code="200">if Password successfully change </response>
        /// <response code="400">If validation failure or current password wrong.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            var result = await _mediator.Send(changePasswordCommand);

            return result.ApiResult;
        }


        /// <summary>
        /// See Your Profile Info
        /// </summary>
        /// <response code="200">profile detail </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _mediator.Send(new GetProfileQuery());

            return Ok(user);
        }



    }
}
