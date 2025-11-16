using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;

namespace NFeValidation.Validators
{
    public class RegrasFiscaisValidator : ValidatorBase
    {
        public override string Name => "RegrasFiscaisValidator";

        protected override Task<ValidationResult> RunAsync(string nfeXml, CancellationToken token)
        {
            // Dummy: cálculo de impostos
            bool ok = true;

            return Task.FromResult(new ValidationResult
            {
                Success = ok,
                ValidatorName = Name,
                Message = ok ? "Cálculo de impostos OK" : "Divergência no cálculo de impostos"
            });
        }
    }
}
