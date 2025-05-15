using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;

namespace GetWatch.Interfaces.SupportTickets
{
    public interface ISupportTicketMapper
    {
        List<ISupportTicket> GetAll(Guid userId);
        ISupportTicket? Get(Guid id);
        void Insert(ISupportTicket supportTicket,DbUser dbUser);
        void Remove(ISupportTicket supportTicket);
    }
}