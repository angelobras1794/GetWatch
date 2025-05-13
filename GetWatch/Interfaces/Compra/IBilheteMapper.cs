using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Interfaces.Compra
{
    public interface IBilheteMapper
    {
        List<IBilhete> GetAll(Guid userId);
        IBilhete? Get(Guid id);
        void Insert(IBilhete supportTicket,Guid userId);
        void Remove(IBilhete supportTicket);
    }
}