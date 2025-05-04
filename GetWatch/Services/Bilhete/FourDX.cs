using System;

namespace GetWatch.Services.Bilhete;
using GetWatch.Interfaces.Compra;
public class FourDX: BilheteDecorator
{
     public FourDX(IBilhete bilhete) : base(bilhete) { }

        public override decimal CalcularPreco()
        {
            return _bilhete.CalcularPreco() + 6.00m;
        }

        public override string Descrever()
        {
            return _bilhete.Descrever() + " em 4DX";
        }

}
