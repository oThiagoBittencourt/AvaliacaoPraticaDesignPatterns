namespace BancoIntegracao.Moderno
{
    public class ProcessadorTransacoesModerno : IProcessadorTransacoes
    {
        public string Autorizar(string cartao, double valor, string moeda)
        {
            return $"Moderno: Autorizado {valor} {moeda} no cartão {cartao}";
        }
    }
}
