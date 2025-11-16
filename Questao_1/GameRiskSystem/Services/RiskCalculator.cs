using GameRiskSystem.Context;
using GameRiskSystem.Strategies;

namespace GameRiskSystem.Services
{
    // Contexto do Strategy Pattern
    // Cada algoritmo pode ser trocado em tempo de execução
    public class RiskCalculator
    {
        private IRiskAlgorithm _algorithm;

        // Injeção do algoritmo → aplica DIP
        public void SetAlgorithm(IRiskAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public string Execute(RiskContext context)
        {
            return _algorithm?.CalculateRisk(context) 
                ?? "Nenhum algoritmo selecionado.";
        }
    }
}
