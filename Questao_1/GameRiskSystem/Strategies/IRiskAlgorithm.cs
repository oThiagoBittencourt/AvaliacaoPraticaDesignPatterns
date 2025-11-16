namespace GameRiskSystem.Strategies
{
    // Interface do padrão Strategy
    // Justificativa: permite trocar algoritmos em runtime e cumpre DIP (SOLID)
    public interface IRiskAlgorithm
    {
        string CalculateRisk(Context.RiskContext context);
    }
}
