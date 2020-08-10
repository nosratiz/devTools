using MediatR;

namespace DevTools.Api.Areas.Admin
{
    public class DashboardController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
