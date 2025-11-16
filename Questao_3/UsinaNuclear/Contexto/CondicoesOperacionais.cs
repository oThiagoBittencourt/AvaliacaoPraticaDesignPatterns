namespace UsinaNuclear.Contexto
{
    public class CondicoesOperacionais
    {
        public double Temperatura { get; set; }
        public double Pressao { get; set; }
        public double Radiacao { get; set; }

        public bool FalhaResfriamento { get; set; }
        public int TempoTemperaturaAltaSegundos { get; set; }
    }
}
