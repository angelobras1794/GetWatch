using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Enums;

namespace GetWatch.Services.ShoppingCart
{
    public class MovieTicketProduct : Product
    {
        private int PersonAmount { get; set; } 
        private string []? Seats { get; set; }

        public new PurchaseType PurchaseType { get; set; } = PurchaseType.Ticket;
        public MovieTicketProduct(double price, int movieId, Guid id,int personAmount,string [] seats) : base(price, movieId, id)
        {
            PersonAmount = personAmount;
            Seats = seats;
        }

        public void setPersonAmount(int personAmount)
        {
            PersonAmount = personAmount;
        }
        public void setSeats(string [] seats)
        {
            Seats = seats;
        }
        public int getPersonAmount()
        {
            return PersonAmount;
        }
        public string []? getSeats()
        {
            return Seats;
        }
    
    }
}