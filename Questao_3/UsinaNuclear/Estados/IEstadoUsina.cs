using UsinaNuclear.Contexto;

namespace UsinaNuclear.Estados
{
    public interface IEstadoUsina
    {
        string Nome { get; }

        // Cada estado decide suas próprias transições
        string Processar(UsinaContext contexto);
    }
}
