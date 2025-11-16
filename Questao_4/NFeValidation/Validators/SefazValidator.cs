using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;

namespace NFeValidation.Validators
{
    public class SefazValidator : ValidatorBase
    {
        public override string Name => "SefazValidator";

        protected override Task<ValidationResult> RunAsync(string nfeXml, CancellationToken token)
        {
            // Dummy: consulta online simulada
            bool ok = true;

            return Task.FromResult(new ValidationResult
            {
                Success = ok,
                ValidatorName = Name,
                Message = ok ? "SEFAZ: consulta OK" : "SEFAZ: rejeitado"
            });
        }
    }
}
