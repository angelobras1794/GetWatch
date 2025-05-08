using System;

namespace GetWatch.Services.Bilhete;
using GetWatch.Interfaces.Compra;
public class IMAX: BilheteDecorator
{
      public IMAX(IBilhete bilhete) : base(bilhete) { }

        public override decimal CalcularPreco()
        {
            return _bilhete.CalcularPreco() + 5.00m;
        }

        public override string Descrever()
        {
            return _bilhete.Descrever() + " em IMAX";
        }

}
