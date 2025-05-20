using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Cards;

namespace GetWatch.Services.Handlers.CardHandlers
{
    public abstract class CardHandler
    {
        protected CardHandler? NextHandler;

        public void SetNext(CardHandler nextHandler)
        {
            NextHandler = nextHandler;
        }

        public abstract void Handle(ICard card);
        
    }
}