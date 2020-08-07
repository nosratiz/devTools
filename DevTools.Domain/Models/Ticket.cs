using System;
using System.Collections.Generic;
using DevTools.Common.Enum;

namespace DevTools.Domain.Models
{
    public class Ticket
    {
        public Ticket()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }

        public virtual User User { get; set; }
        public virtual Ticket Parent { get; set; }
        public virtual ICollection<Ticket> Tickets { get; }

    }
}