using System;
using DevTools.Application.Tickets.ModelDto;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Tickets.Command.ReplyTicketCommand
{
    public class ReplyTicketCommand : IRequest<Result<TicketDto>>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }
    }
}