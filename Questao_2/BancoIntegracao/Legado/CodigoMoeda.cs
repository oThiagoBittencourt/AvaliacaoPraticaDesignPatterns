namespace BancoIntegracao.Legado
{
    public static class CodigoMoeda
    {
        public static int Converter(string moeda)
        {
            return moeda.ToUpper() switch
            {
                "USD" => 1,
                "EUR" => 2,
                "BRL" => 3,
                _ => throw new Exception("Moeda não suportada pelo legado.")
            };
        }
    }
}
