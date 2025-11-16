using System;
using BancoIntegracao.Adapter;
using BancoIntegracao.Legado;

namespace BancoIntegracao
{
    class Program
    {
        static void Main()
        {
            var legado = new SistemaBancarioLegado();
            var adapter = new TransacaoAdapter(legado);

            // Cliente moderno chamando o legado automaticamente
            string resultado = adapter.Autorizar("1234-5678", 100.00, "USD");
            Console.WriteLine(resultado);
        }
    }
}
