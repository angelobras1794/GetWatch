using System;

namespace GetWatch.Services.MovieTicket;
using GetWatch.Interfaces.Compra;
public class MultiplePersonTicket: MovieTicketDecorator
{
      private int _ammount;

        public MultiplePersonTicket(IMovieTicket ticket, int ammount) : base(ticket)
        {
            _ammount = ammount;
        }

        public override double CalculatePrice()
        {
            return _bilhete.CalculatePrice() * _ammount;
        }

        public override string Description()
        {
            return $"{_bilhete.Description()} para {_ammount} pessoa(s)";
        }

}
