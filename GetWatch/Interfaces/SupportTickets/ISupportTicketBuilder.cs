using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Interfaces.SupportTickets
{
    public interface ISupportTicketBuilder
    {
        ISupportTicketBuilder SetSubject(string subject);
        ISupportTicketBuilder SetDescription(string description);
        ISupportTicketBuilder SetResolved(bool isResolved);
        ISupportTicketBuilder SetId(Guid id);
        ISupportTicket Build();
        
    }
}