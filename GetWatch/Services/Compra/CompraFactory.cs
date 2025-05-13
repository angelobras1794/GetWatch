using System;
using GetWatch.Interfaces.Compra;

namespace GetWatch.Services.Compra
{
    public static class CompraFactory
    {
        public static ICompra CriarCompra(string tipo)
        {
            switch (tipo.ToLower())
            {
                case "Ticket":
                    return new ReservarBilhete();
                case "BlueRay":
                    return new ComprarFilme();
                case "Rent":
                    return new AluguarFilme();
                default:
                    throw new ArgumentException($"Tipo de compra inválido: {tipo}");
            }
        }
    }
}
