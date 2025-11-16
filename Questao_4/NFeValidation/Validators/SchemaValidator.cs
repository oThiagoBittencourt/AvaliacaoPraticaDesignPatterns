using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;

namespace NFeValidation.Validators
{
    public class SchemaValidator : ValidatorBase
    {
        public override string Name => "SchemaValidator";

        protected override Task<ValidationResult> RunAsync(string nfeXml, CancellationToken token)
        {
            // Dummy: simula validação XSD
            return Task.FromResult(new ValidationResult
            {
                Success = true,
                ValidatorName = Name,
                Message = "XML compatível com XSD"
            });
        }
    }
}
