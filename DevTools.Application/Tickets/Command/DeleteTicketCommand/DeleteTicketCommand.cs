using System;
using DevTools.Application.Tickets.ModelDto;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Tickets.Command.DeleteTicketCommand
{
    public class DeleteTicketCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}