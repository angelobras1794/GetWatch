using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Interfaces.Cards
{
    public interface ICard
    {
        public long CardNumber { get; set; }
        public string CardOwner { get; set; }
        public string ExpiryDate { get; set; }
        public int Cvv { get; set; }
        public Guid Id { get; set; }
        
        bool IsExpired();
        
    }
}