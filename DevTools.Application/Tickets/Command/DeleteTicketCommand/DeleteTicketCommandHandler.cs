using System.Threading;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.General;
using DevTools.Common.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTools.Application.Tickets.Command.DeleteTicketCommand
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Result>
    {

        private readonly IDevToolsDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTicketCommandHandler(IDevToolsDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.SingleOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id,
                cancellationToken);

            if (ticket is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TicketWasNotFound)));

            if (ticket.UserId.ToString() != _currentUserService.UserId)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.ChangeNotAllowed)));

            ticket.IsDeleted = true;

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage()));
        }
    }
}