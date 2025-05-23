using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Cards;

namespace GetWatch.Services.Handlers.CardHandlers
{
    public class CardOwnerHandler : CardHandler
    {
        public CardOwnerHandler()
        {
            
        }

        public override void Handle(ICard card)
        {
            if (string.IsNullOrEmpty(card.CardOwner) || card.CardOwner.Length < 3)
            {
                throw new Exception("Card owner name must be at least 3 characters long.");
            }

            
            NextHandler?.Handle(card);
        }

        
    }
}