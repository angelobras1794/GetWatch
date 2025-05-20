using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Cards;

namespace GetWatch.Services.Handlers.CardHandlers
{
    public class CvvHandler : CardHandler
    {
        public CvvHandler()
        {
            
        }

        public override void Handle(ICard card)
        {
            if (card.Cvv.ToString().Length < 3 || card.Cvv.ToString().Length > 4 )
            {
                throw new Exception("CVV must be 3 or 4 digits long and contain only numbers.");

            }

            // Pass to the next handler
            NextHandler?.Handle(card);
        }
        
    }
}