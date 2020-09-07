using System.Threading.Tasks;
using DevTools.Application.Transactions.Dto;
using DevTools.Application.Transactions.Queries;
using DevTools.Application.Users.Model;
using DevTools.Common.General;
using DevTools.Common.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Api.Areas.Admin
{
    public class TransactionController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// transaction List
        /// </summary>
        /// <param name="transactionPagedList"></param>
        /// <returns>User List</returns>
        /// <response code="200">if Get List successfully </response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(PagedList<TransactionDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet]
        public async Task<IActionResult> GetTransaction([FromQuery] GetTransactionPagedListQuery transactionPagedList)
            => Ok(await _mediator.Send(transactionPagedList));
    }
}
