using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Tickets.Command.EditTicketCommand
{
    public class EditTicketCommandHandler : IRequestHandler<EditTicketCommand, Result>
    {
        private readonly IDevToolsDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public EditTicketCommandHandler(IDevToolsDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result> Handle(EditTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id,
                cancellationToken);

            if (ticket is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TicketWasNotFound)));

            if (ticket.UserId.ToString() != _currentUserService.UserId)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ChangeNotAllowed)));

            _mapper.Map(request, ticket);

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.TicketEditSuccessfully)));
        }
    }
}