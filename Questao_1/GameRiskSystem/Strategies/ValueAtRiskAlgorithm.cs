using GameRiskSystem.Context;

namespace GameRiskSystem.Strategies
{
    public class ValueAtRiskAlgorithm : IRiskAlgorithm
    {
        public string CalculateRisk(RiskContext context)
        {
            return $"[VaR] Risco de perder loot: {context.LootValue * 0.3:F2}";
        }
    }
}
