using UsinaNuclear.Contexto;

namespace UsinaNuclear.Estados
{
    public class EstadoEmergencia : IEstadoUsina
    {
        public string Nome => "EMERGENCIA";

        public string Processar(UsinaContext contexto)
        {
            // Não há retorno automático; exige intervenção humana
            return "!!! ESTADO DE EMERGÊNCIA ATIVO !!!";
        }
    }
}
