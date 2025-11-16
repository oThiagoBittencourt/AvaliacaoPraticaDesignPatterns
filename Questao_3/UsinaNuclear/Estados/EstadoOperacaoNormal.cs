using UsinaNuclear.Contexto;

namespace UsinaNuclear.Estados
{
    public class EstadoOperacaoNormal : IEstadoUsina
    {
        public string Nome => "OPERACAO_NORMAL";

        public string Processar(UsinaContext contexto)
        {
            var c = contexto.Condicoes;

            if (c.Temperatura > 300)
            {
                contexto.AlterarEstado(new EstadoAlertaAmarelo());
                return "Transição: OPERACAO_NORMAL → ALERTA_AMARELO.";
            }

            return "Operação normal estável.";
        }
    }
}
