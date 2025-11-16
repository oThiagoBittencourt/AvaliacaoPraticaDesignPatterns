using UsinaNuclear.Contexto;

namespace UsinaNuclear.Estados
{
    public class EstadoDesligada : IEstadoUsina
    {
        public string Nome => "DESLIGADA";

        public string Processar(UsinaContext contexto)
        {
            // Liga a usina → operação normal
            contexto.AlterarEstado(new EstadoOperacaoNormal());
            return "Usina ligada. Mudando para OPERACAO_NORMAL.";
        }
    }
}
