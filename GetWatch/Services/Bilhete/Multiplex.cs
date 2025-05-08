using System;

namespace GetWatch.Services.Bilhete;
using GetWatch.Interfaces.Compra;
public class Multiplex: BilheteDecorator
{
       public Multiplex(IBilhete bilhete) : base(bilhete) { }

        public override decimal CalcularPreco()
        {
            return _bilhete.CalcularPreco() + 2.00m;
        }

        public override string Descrever()
        {
            return _bilhete.Descrever() + " no Multiplex";
        }

}
