using System;
using GetWatch.Interfaces.Compra;

namespace GetWatch.Services.MovieTicket;



public class BaseMovieTicket: IMovieTicket

{

        
        public virtual double CalculatePrice()
        {
        return 10.00;
        }

        public virtual string Description()
        {
            return "Standard Movie Ticket";
        }

}
