using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Cards;

namespace GetWatch.Services.Handlers.CardHandlers
{
    public class CardNumberHandler : CardHandler
    {

        public CardNumberHandler()
        {
            
        }

        public override void Handle(ICard card)
        {
            if (card.CardNumber == null || card.CardNumber.Length != 16)
            {
                throw new Exception("Card number must be 16 digits.");
            }

            // Pass to the next handler
            NextHandler?.Handle(card);
        }
        
    }
}