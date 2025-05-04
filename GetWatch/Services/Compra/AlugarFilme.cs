using System;

using GetWatch.Interfaces.Compra;
using GetWatch.Services.Compra;

    public class AluguarFilme : ICompra
    {
        public string ProcessarCompra()
        {
            return "Filme alugado com sucesso.";
        }
    }
