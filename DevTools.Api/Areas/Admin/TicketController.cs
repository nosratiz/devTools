using System;
using System.Threading.Tasks;
using DevTools.Application.Tickets.Command.DeleteTicketCommand;
using DevTools.Application.Tickets.Command.EditTicketCommand;
using DevTools.Application.Tickets.Command.ReplyTicketCommand;
using DevTools.Application.Tickets.ModelDto;
using DevTools.Application.Tickets.Queries;
using DevTools.Common.General;
using DevTools.Common.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Admin
{
    [Authorize(Roles = "Admin,Supporter")]
    public class TicketController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Ticket List
        /// </summary>
        /// <param name="filterTicketListQuery"></param>
        /// <returns>Ticket List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<TicketDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> GetTicketList([FromQuery] GetTicketPagedListQuery filterTicketListQuery)
        {
            var result = await _mediator.Send(filterTicketListQuery);

            return result.ApiResult;
        }


        /// <summary>
        /// Get Ticket 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ticket </returns>
        /// <response code="200">if ticket found successfully </response>
        /// <response code="404">if ticket Not Found </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<TicketDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetTicketInfo")]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            var result = await _mediator.Send(new GetTicketQuery { Id = id });

            return result.ApiResult;
        }


        /// <summary>
        /// Reply Ticket By Admin 
        /// </summary>
        /// <param name="replyTicketCommand"></param>
        /// <returns>Reply Ticket </returns>
        /// <response code="201">if ticket Reply successfully </response>
        /// <response code="404">if ticket Not Found </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 201)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> ReplyTicket(ReplyTicketCommand replyTicketCommand)
        {
            var result = await _mediator.Send(replyTicketCommand);

            if (result.Success == false)
                return result.ApiResult;

            return Created(Url.Link("GetTicketInfo", new { id = result.Data.Id }), result.Data);
        }

        /// <summary>
        /// Delete Ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Ticket </returns>
        /// <response code="200">if ticket Delete successfully </response>
        /// <response code="404">if ticket Not Found </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTicketCommand {Id = id});

            return result.ApiResult;
        }

        /// <summary>
        /// Edit Your Reply Ticket
        /// </summary>
        /// <param name="editTicketCommand"></param>
        /// <returns>Edit Your Reply Ticket </returns>
        /// <response code="200">if ticket Edit successfully </response>
        /// <response code="404">if ticket Not Found </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 404)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> EditTicket(EditTicketCommand editTicketCommand)
        {
            var result = await _mediator.Send(editTicketCommand);

            return result.ApiResult;
        }


    }
}
