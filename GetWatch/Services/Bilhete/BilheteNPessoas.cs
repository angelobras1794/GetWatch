using System;

namespace GetWatch.Services.Bilhete;
using GetWatch.Interfaces.Compra;
public class BilheteNPessoas: BilheteDecorator
{
    private int _quantidade;

        public BilheteNPessoas(IBilhete bilhete, int quantidade) : base(bilhete)
        {
            _quantidade = quantidade;
        }

        public override decimal CalcularPreco()
        {
            return _bilhete.CalcularPreco() * _quantidade;
        }

        public override string Descrever()
        {
            return $"{_bilhete.Descrever()} para {_quantidade} pessoa(s)";
        }

}
