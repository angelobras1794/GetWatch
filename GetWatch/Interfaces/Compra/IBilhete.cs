using System;

namespace GetWatch.Interfaces.Compra;

public interface IBilhete
{
    public int PersonAmount { get; set; } 
        public string []? Seats { get; set; }
         public Guid Id { get; set; }

     decimal CalcularPreco();
    string Descrever();

}
