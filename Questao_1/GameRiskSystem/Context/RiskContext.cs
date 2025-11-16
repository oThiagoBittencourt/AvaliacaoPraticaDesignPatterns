namespace GameRiskSystem.Context
{
    // Representa o conjunto de parâmetros complexos compartilhados entre estratégias.
    public class RiskContext
    {
        public int PlayerLevel { get; set; }
        public int EnemyPower { get; set; }
        public int LootValue { get; set; }
        public double WeatherModifier { get; set; }
    }
}
