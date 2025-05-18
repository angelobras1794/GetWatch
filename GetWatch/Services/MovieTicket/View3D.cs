using System;

namespace GetWatch.Services.MovieTicket;
using GetWatch.Interfaces.Compra;
public class View3D: MovieTicketDecorator
{
        
        public View3D(IMovieTicket bilhete) : base(bilhete) { }

        public override double CalculatePrice()
        {
            return _bilhete.CalculatePrice() + 3.00;
        }

        public override string Description()
        {
            return _bilhete.Description() + " with 3D";
        }
    

}
