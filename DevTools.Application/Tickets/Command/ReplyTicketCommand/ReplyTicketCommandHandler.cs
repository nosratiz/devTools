using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Application.Tickets.ModelDto;
using DevTools.Common.General;
using DevTools.Common.Result;
using DevTools.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Tickets.Command.ReplyTicketCommand
{
    public class ReplyTicketCommandHandler : IRequestHandler<ReplyTicketCommand, Result<TicketDto>>
    {
        private readonly IDevToolsDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ReplyTicketCommandHandler(IDevToolsDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<TicketDto>> Handle(ReplyTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id,
                cancellationToken);

            if (ticket is null)
                return Result<TicketDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TicketWasNotFound)));

            var replyTicket = _mapper.Map<Ticket>(request);

            replyTicket.ParentId = request.Id;
            replyTicket.UserId = Guid.Parse(_currentUserService.UserId);
            replyTicket.Title = ticket.Title;


            await _context.Tickets.AddAsync(replyTicket, cancellationToken);

            await _context.SaveAsync(cancellationToken);

            return Result<TicketDto>.SuccessFul(_mapper.Map<TicketDto>(replyTicket));
        }
    }
}