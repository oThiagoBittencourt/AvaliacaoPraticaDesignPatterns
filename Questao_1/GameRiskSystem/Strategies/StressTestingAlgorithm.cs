using GameRiskSystem.Context;

namespace GameRiskSystem.Strategies
{
    public class StressTestingAlgorithm : IRiskAlgorithm
    {
        public string CalculateRisk(RiskContext context)
        {
            return $"[Stress] Impacto do clima no risco: {(context.WeatherModifier * context.EnemyPower):F1}";
        }
    }
}
