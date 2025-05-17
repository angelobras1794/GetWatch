using System;

namespace GetWatch.Services.MovieTicket;
using GetWatch.Interfaces.Compra;
public abstract class MovieTicketDecorator : IMovieTicket
{
        public IMovieTicket _bilhete;

        public int PersonAmount
        {
            get => _bilhete.PersonAmount;
            set => _bilhete.PersonAmount = value;
        }


        public MovieTicketDecorator(IMovieTicket bilhete)
        {
            _bilhete = bilhete;
        }

        public virtual double CalculatePrice()
        {
            return _bilhete.CalculatePrice();
        }

        public virtual string Description()
        {
            return _bilhete.Description();
        }
}