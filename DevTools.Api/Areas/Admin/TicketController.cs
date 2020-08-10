using MediatR;
using Microsoft.AspNetCore.Authorization;

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
    }
}
