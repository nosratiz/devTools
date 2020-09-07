using System;
using DevTools.Common.Result;
using MediatR;

namespace DevTools.Application.Tickets.Command.EditTicketCommand
{
    public class EditTicketCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }
    }
}