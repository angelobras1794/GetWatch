using System;

namespace GetWatch.Services.MovieTicket;
using GetWatch.Interfaces.Compra;
public class FourDX: MovieTicketDecorator
{
     public FourDX(IMovieTicket ticket) : base(ticket) { }

        public override double CalculatePrice()
        {
            return _bilhete.CalculatePrice() + 6.00;
        }

        public override string Description()
        {
            return _bilhete.CalculatePrice() + " in 4DX";
        }

}
