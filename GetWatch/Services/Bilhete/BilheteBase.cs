using System;

namespace GetWatch.Services.Bilhete;

using GetWatch.Interfaces.Compra;

public class BilheteBase: IBilhete

{
    public int PersonAmount { get; set; }
    public string[]? Seats { get; set; } 

    public Guid Id { get; set; }

        public BilheteBase()
        {
            Id = Guid.NewGuid();
        }

       
        public BilheteBase(int personAmount, string[]? seats)
        {
            PersonAmount = personAmount;
            Seats = seats;
            Id = Guid.NewGuid();
        }
     public virtual decimal CalcularPreco()
        {
            return 10.00m; // preço base
        }

        public virtual string Descrever()
        {
            return "Bilhete padrão";
        }

}
