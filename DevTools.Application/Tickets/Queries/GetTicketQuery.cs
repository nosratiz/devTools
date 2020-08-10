using System;
using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Tickets.ModelDto;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Tickets.Queries
{
    public class GetTicketQuery : IRequest<Result<TicketDto>>
    {
        public Guid Id { get; set; }
    }


    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, Result<TicketDto>>
    {
        public Task<Result<TicketDto>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}