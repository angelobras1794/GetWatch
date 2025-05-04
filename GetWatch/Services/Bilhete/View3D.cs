using System;

namespace GetWatch.Services.Bilhete;
using GetWatch.Interfaces.Compra;
public class View3D: BilheteDecorator
{
        
        public View3D(IBilhete bilhete) : base(bilhete) { }

        public override decimal CalcularPreco()
        {
            return _bilhete.CalcularPreco() + 3.00m;
        }

        public override string Descrever()
        {
            return _bilhete.Descrever() + " com 3D";
        }
    

}
