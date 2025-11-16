namespace BancoIntegracao.Moderno
{
    public interface IProcessadorTransacoes
    {
        string Autorizar(string cartao, double valor, string moeda);
    }
}
