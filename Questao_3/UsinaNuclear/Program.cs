using System;
using UsinaNuclear.Contexto;
using UsinaNuclear.Estados;

namespace UsinaNuclear
{
    class Program
    {
        static void Main()
        {
            var usina = new UsinaContext();

            usina.Processar(); // liga a usina

            // Simulando condições (dummy)
            usina.Condicoes.Temperatura = 350;
            Console.WriteLine(usina.Processar());

            usina.Condicoes.Temperatura = 410;
            usina.Condicoes.TempoTemperaturaAltaSegundos = 40;
            Console.WriteLine(usina.Processar());

            usina.Condicoes.FalhaResfriamento = true;
            Console.WriteLine(usina.Processar());

            // Modo de manutenção (sobrescreve tudo)
            usina.AlterarEstado(new EstadoManutencao());
            Console.WriteLine(usina.Processar());
        }
    }
}
