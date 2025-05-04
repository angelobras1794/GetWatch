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
                case "bilhete":
                    return new ReservarBilhete();
                case "comprar":
                    return new ComprarFilme();
                case "alugar":
                    return new AluguarFilme();
                default:
                    throw new ArgumentException($"Tipo de compra inválido: {tipo}");
            }
        }
    }
}
