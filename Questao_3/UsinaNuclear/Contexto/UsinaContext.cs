using UsinaNuclear.Estados;

namespace UsinaNuclear.Contexto
{
    public class UsinaContext
    {
        public IEstadoUsina EstadoAtual { get; private set; }
        public CondicoesOperacionais Condicoes { get; } = new CondicoesOperacionais();

        public UsinaContext()
        {
            EstadoAtual = new EstadoDesligada();
        }

        public void AlterarEstado(IEstadoUsina novoEstado)
        {
            EstadoAtual = novoEstado;
        }

        public string Processar()
        {
            return EstadoAtual.Processar(this);
        }
    }
}
