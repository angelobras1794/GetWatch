using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Interfaces.SupportTickets
{
    public interface ISupportTicketMapper
    {
        List<ISupportTicket> GetAll(Guid userId);
        ISupportTicket? Get(Guid id);
        void Insert(ISupportTicket supportTicket,Guid userId);
        void Remove(ISupportTicket supportTicket);
    }
}