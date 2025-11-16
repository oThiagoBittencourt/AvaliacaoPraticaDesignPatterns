using UsinaNuclear.Contexto;

namespace UsinaNuclear.Estados
{
    public class EstadoManutencao : IEstadoUsina
    {
        public string Nome => "MANUTENCAO";

        public string Processar(UsinaContext contexto)
        {
            return "Sistema em manutenção — transições normais suspensas.";
        }
    }
}
