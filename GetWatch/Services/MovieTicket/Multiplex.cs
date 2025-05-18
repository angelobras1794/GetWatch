using System;

namespace GetWatch.Services.MovieTicket;
using GetWatch.Interfaces.Compra;
public class Multiplex: MovieTicketDecorator
{
       public Multiplex(IMovieTicket bilhete) : base(bilhete) { }

        public override double CalculatePrice()
        {
            return _bilhete.CalculatePrice() + 2.00;
        }

        public override string Description()
        {
        return _bilhete.CalculatePrice() + " in Multiplex";
        }

}
