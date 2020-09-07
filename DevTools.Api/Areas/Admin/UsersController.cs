using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevTools.Application.Templates.Dto;
using DevTools.Application.Templates.Queries;
using DevTools.Application.UserApplications.Dto;
using DevTools.Application.UserApplications.Queries;
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
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<UserDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PagingOptions pagingOptions)
        {
            var result = await _mediator.Send(new GetUserPagedListQuery
            {
                Limit = pagingOptions.Limit,
                Page = pagingOptions.Page,
                Query = pagingOptions.Query
            });

            return result.ApiResult;
        }


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
        [ProducesResponseType(typeof(ApiMessage), 200)]
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
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });

            return result.ApiResult;
        }

        /// <summary>
        /// Return list of Group Template For User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pagingOptions"></param>
        /// <response code="200">Group Template List </response>
        /// <response code="404">If entity not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<GroupTemplateDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}/groupTemplate")]
        public async Task<IActionResult> GetUserGroupTemplate(Guid id, [FromQuery] PagingOptions pagingOptions)
        {
            var result = await _mediator.Send(new GetGroupTemplateListQuery
            {
                UserId = id,
                Page = pagingOptions.Page,
                Limit = pagingOptions.Limit,
                Query = pagingOptions.Query
            });

            return result.ApiResult;
        }


        /// <summary>
        /// Return list of templates for user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupId"></param>
        /// <response code="200"> Template List </response>
        /// <response code="404">If user not found or template.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<TemplateDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}/groupTemplate/{groupId}/Templates")]
        public async Task<IActionResult> GetUserGroupTemplate(Guid id, Guid groupId)
        {
            var result = await _mediator.Send(new GetUserTemplateListQuery { UserId = id, GroupTemplateId = groupId });

            return result.ApiResult;
        }


        /// <summary>
        /// Return list of Application Register 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Application List </response>
        /// <response code="404">If user not found .</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(List<UserApplicationDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}/Applications")]
        public async Task<IActionResult> GetUserApplication(Guid id)
        {
            var result = await _mediator.Send(new GetUserApplicationListQuery { UserId = id });

            return result.ApiResult;
        }

    }
}
