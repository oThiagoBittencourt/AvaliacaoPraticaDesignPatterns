using UsinaNuclear.Contexto;

namespace UsinaNuclear.Estados
{
    public class EstadoAlertaVermelho : IEstadoUsina
    {
        public string Nome => "ALERTA_VERMELHO";

        public string Processar(UsinaContext contexto)
        {
            var c = contexto.Condicoes;

            if (c.FalhaResfriamento)
            {
                // Regra: emergência só vem DEPOIS de alerta vermelho
                contexto.AlterarEstado(new EstadoEmergencia());
                return "Transição: ALERTA_VERMELHO → EMERGENCIA.";
            }

            if (c.Temperatura <= 400)
            {
                contexto.AlterarEstado(new EstadoAlertaAmarelo());
                return "Retorno seguro: ALERTA_VERMELHO → ALERTA_AMARELO.";
            }

            return "Alerta Vermelho mantido.";
        }
    }
}
