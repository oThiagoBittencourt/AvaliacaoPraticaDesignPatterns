using UsinaNuclear.Contexto;

namespace UsinaNuclear.Estados
{
    public class EstadoAlertaAmarelo : IEstadoUsina
    {
        public string Nome => "ALERTA_AMARELO";

        public string Processar(UsinaContext contexto)
        {
            var c = contexto.Condicoes;

            if (c.Temperatura > 400 && c.TempoTemperaturaAltaSegundos > 30)
            {
                contexto.AlterarEstado(new EstadoAlertaVermelho());
                return "Transição: ALERTA_AMARELO → ALERTA_VERMELHO.";
            }

            // Permite retornar para operação normal (bidirecional)
            if (c.Temperatura <= 300)
            {
                contexto.AlterarEstado(new EstadoOperacaoNormal());
                return "Retorno: ALERTA_AMARELO → OPERACAO_NORMAL.";
            }

            return "Estado de ALERTA AMARELO mantido.";
        }
    }
}
