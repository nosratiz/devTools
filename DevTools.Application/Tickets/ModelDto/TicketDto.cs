using System;
using System.Collections.Generic;
using DevTools.Common.Enum;

namespace DevTools.Application.Tickets.ModelDto
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }

        public virtual List<TicketDto> Children { get; set; }
    }
}