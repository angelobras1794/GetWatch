using System;

namespace GetWatch.Services.Bilhete;

using GetWatch.Interfaces.Compra;

public class BilheteBase: IBilhete
{
     public virtual decimal CalcularPreco()
        {
            return 10.00m; // preço base
        }

        public virtual string Descrever()
        {
            return "Bilhete padrão";
        }

}
