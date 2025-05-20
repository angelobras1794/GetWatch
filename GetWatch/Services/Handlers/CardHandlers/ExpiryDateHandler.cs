using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Cards;

namespace GetWatch.Services.Handlers.CardHandlers
{
    public class ExpiryDateHandler : CardHandler
    {
        public ExpiryDateHandler()
        {

        }

        public override void Handle(ICard card)
        {
            if (string.IsNullOrEmpty(card.ExpiryDate) || 
                !DateTime.TryParse(card.ExpiryDate, out DateTime expiryDateValue) || 
                expiryDateValue < DateTime.Now)
            {
                throw new Exception("Card expiry date must be a valid date in the future.");
            }

            NextHandler?.Handle(card);

        }
    }
}