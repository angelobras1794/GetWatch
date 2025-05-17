using System;

namespace GetWatch.Services.MovieTicket;
using GetWatch.Interfaces.Compra;
public class IMAX: MovieTicketDecorator
{
      public IMAX(IMovieTicket ticket) : base(ticket) { }

        public override double CalculatePrice()
        {
            return _bilhete.CalculatePrice() + 5.00;
        }

        public override string Description()
        {
            return _bilhete.Description() + " in IMAX";
        }

}
