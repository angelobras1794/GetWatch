using System;

namespace GetWatch.Services.Bilhete;
using GetWatch.Interfaces.Compra;
public abstract class BilheteDecorator : IBilhete
{
        public IBilhete _bilhete;

        public int PersonAmount
        {
            get => _bilhete.PersonAmount;
            set => _bilhete.PersonAmount = value;
        }
        public string[]? Seats
        {
            get => _bilhete.Seats;
            set => _bilhete.Seats = value;
        }

        public Guid Id
        {
            get => _bilhete.Id;
            set => _bilhete.Id = value;
        }

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