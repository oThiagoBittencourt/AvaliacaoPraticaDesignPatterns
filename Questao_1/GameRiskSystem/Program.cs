using GameRiskSystem.Context;
using GameRiskSystem.Services;
using GameRiskSystem.Strategies;

namespace GameRiskSystem
{
    class Program
    {
        static void Main()
        {
            var context = new RiskContext
            {
                PlayerLevel = 10,
                EnemyPower = 18,
                LootValue = 500,
                WeatherModifier = 1.4
            };

            var calculator = new RiskCalculator();

            calculator.SetAlgorithm(new ValueAtRiskAlgorithm());
            Console.WriteLine(calculator.Execute(context));

            calculator.SetAlgorithm(new ExpectedShortfallAlgorithm());
            Console.WriteLine(calculator.Execute(context));

            calculator.SetAlgorithm(new StressTestingAlgorithm());
            Console.WriteLine(calculator.Execute(context));
        }
    }
}
