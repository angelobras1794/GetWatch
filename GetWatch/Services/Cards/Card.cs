using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Cards;

namespace GetWatch.Services.Cards
{
    public class Card : ICard
    {
        public long CardNumber { get; set; }
        public string CardOwner { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public int Cvv { get; set; }
        public Guid Id { get; set; } 


        public Card(long cardNumber, string cardOwner, string expiryDate, int cvv, Guid id = new Guid () )
        {
            CardNumber = cardNumber;
            CardOwner = cardOwner;
            ExpiryDate = expiryDate;
            Cvv = cvv;
            Id = id;
        }

        public bool IsExpired()
        {
            DateTime expiryDate = DateTime.Parse(ExpiryDate);
            return expiryDate < DateTime.Now;
        }
    }
}