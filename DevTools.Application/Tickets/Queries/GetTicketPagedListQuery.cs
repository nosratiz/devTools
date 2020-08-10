using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Tickets.ModelDto;
using DevTools.Common.Enum;
using DevTools.Common.General;
using DevTools.Common.Helper;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Tickets.Queries
{
    public class GetTicketPagedListQuery : PagingOptions, IRequest<Result<PagedList<TicketDto>>>
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public TicketStatus? TicketStatus { get; set; }
    }

    public class GetTicketPagedListQueryHandler : PagingService<Ticket>, IRequestHandler<GetTicketPagedListQuery, Result<PagedList<TicketDto>>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;

        public GetTicketPagedListQueryHandler(IDevToolsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<TicketDto>>> Handle(GetTicketPagedListQuery request, CancellationToken cancellationToken)
        {
            var fromDate = request.FromDate ?? new DateTime();
            var toDate = request.ToDate ?? DateTime.Now;

            IQueryable<Ticket> tickets = _context.Tickets
                .Include(x => x.User)
                .Where(x => x.IsDeleted == false && 
                            x.ParentId == null &&
                            x.CreateDate >= fromDate &&
                            x.CreateDate <= toDate)
                .OrderByDescending(x => x.ModifiedDate);


            if (!string.IsNullOrWhiteSpace(request.Query))
                tickets = tickets.Where(x => x.User.Name.Contains(request.Query)
                                             || x.User.Family.Contains(request.Query)
                                             || x.Title.Contains(request.Query));

            if (request.TicketStatus.HasValue)
                tickets = tickets.Where(x => x.TicketStatus == request.TicketStatus.Value);

            var ticketList = await GetPagedAsync(request.Page, request.Limit, tickets, cancellationToken);

            return Result<PagedList<TicketDto>>.SuccessFul(ticketList.MapTo<TicketDto>(_mapper));

        }
    }
}