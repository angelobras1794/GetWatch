using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Enums;
using GetWatch.Interfaces.Compra;

namespace GetWatch.Services.ShoppingCart
{
    public class MovieTicketProduct : Product
    {
        private int PersonAmount { get; set; } 
        private string []? Seats { get; set; }

        public  PurchaseType PurchaseType { get; set; } = PurchaseType.Ticket;
        public MovieTicketProduct(double price, int movieId,int Quantity ,Guid id,int personAmount,string [] seats) : base(price, movieId,Quantity ,id)
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