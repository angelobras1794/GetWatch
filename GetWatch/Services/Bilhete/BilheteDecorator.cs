using System;

namespace GetWatch.Services.Bilhete;
using GetWatch.Interfaces.Compra;
public abstract class BilheteDecorator : IBilhete
{
        public IBilhete _bilhete;

        public BilheteDecorator(IBilhete bilhete)
        {
            _bilhete = bilhete;
        }

        public virtual decimal CalcularPreco()
        {
            return _bilhete.CalcularPreco();
        }

        public virtual string Descrever()
        {
            return _bilhete.Descrever();
        }
}