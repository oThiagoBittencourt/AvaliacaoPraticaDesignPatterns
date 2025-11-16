using GameRiskSystem.Context;

namespace GameRiskSystem.Strategies
{
    public class ExpectedShortfallAlgorithm : IRiskAlgorithm
    {
        public string CalculateRisk(RiskContext context)
        {
            return $"[ES] Perda esperada sob falha crítica: {(context.EnemyPower - context.PlayerLevel) * 2}";
        }
    }
}
