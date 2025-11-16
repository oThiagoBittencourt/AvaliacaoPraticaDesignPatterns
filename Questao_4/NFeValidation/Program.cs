using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NFeValidation.Pipeline;
using NFeValidation.Validators;

namespace NFeValidation
{
    class Program
    {
        static async Task Main()
        {
            // Ordem imposta: 1.Schema, 2.Cert, 3.RegrasFiscais, 4.DB, 5.Sefaz
            var validators = new List<IValidator>
            {
                new SchemaValidator(),
                new CertificadoValidator(),
                new RegrasFiscaisValidator(),
                new DatabaseValidator(),     // transacional: precisa de rollback se algo falhar depois
                new SefazValidator()
            };

            // Configura regra condicional: se Certificado falhar, pule Sefaz (exemplo)
            var skipMap = new Dictionary<string, string>
            {
                { "CertificadoValidator", "SefazValidator" }
            };

            var pipeline = new ValidationPipeline(validators, skipMap, maxFailuresBeforeBreak: 3);

            // Configura timeouts (ms) individuais
            var timeouts = new Dictionary<string, int>
            {
                { "SchemaValidator", 2000 },
                { "CertificadoValidator", 2000 },
                { "RegrasFiscaisValidator", 3000 },
                { "DatabaseValidator", 4000 },
                { "SefazValidator", 5000 }
            };

            string dummyNFe = "<nfe>...</nfe>";

            var results = await pipeline.ExecuteAsync(dummyNFe, timeouts);

            Console.WriteLine("Resultados da validação:");
            foreach (var r in results)
            {
                Console.WriteLine($"{r.ValidatorName}: {(r.Success ? "OK" : "FALHA")} - {r.Message}");
            }
        }
    }
}
