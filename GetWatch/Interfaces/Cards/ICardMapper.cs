using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Interfaces.Cards
{
    public interface ICardMapper
    {
        List<ICard> GetAll(Guid UserId);
        ICard? Get(Guid id);

        void Insert(ICard card, Guid UserId);
        void Remove(ICard card);

        
    }
}