using System;
using System.Threading.Tasks;
using DevTools.Application.Users.Command.CreateUser;
using DevTools.Application.Users.Command.DeleteUser;
using DevTools.Application.Users.Command.UpdateUser;
using DevTools.Application.Users.Model;
using DevTools.Application.Users.Queries;
using DevTools.Common.General;
using DevTools.Common.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// User List
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <returns>User List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<UserDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PagingOptions pagingOptions)
        => Ok(await _mediator.Send(new GetUserPagedListQuery
        {
            Limit = pagingOptions.Limit,
            Page = pagingOptions.Page,
            Query = pagingOptions.Query
        }));


        /// <summary>
        /// User Profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Profile</returns>
        /// <response code="200">if Get successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "UserInfo")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _mediator.Send(new GetUserQuery { Id = id });

            return result.ApiResult;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="createUserCommand"></param>
        /// <returns>NoContent</returns>
        /// <response code="201">if Create successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("UserInfo", new { id = result.Data.Id }), result.Data);

        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="updateUserCommand"></param>
        /// <returns>NoContent</returns>
        /// <response code="200">if Update successfully </response>
        /// <response code="400">If validation failure.</response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage),200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            var result = await _mediator.Send(updateUserCommand);

            return result.ApiResult;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent</returns>
        /// <response code="200">if Delete successfully </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand {Id = id});

            return result.ApiResult;
        }
    }
}
