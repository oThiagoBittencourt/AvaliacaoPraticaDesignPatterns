using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;

namespace NFeValidation.Validators
{
    public class CertificadoValidator : ValidatorBase
    {
        public override string Name => "CertificadoValidator";

        protected override Task<ValidationResult> RunAsync(string nfeXml, CancellationToken token)
        {
            // Dummy: simula verificação de expiração/revogação
            bool ok = true; // ajuste para testes

            return Task.FromResult(new ValidationResult
            {
                Success = ok,
                ValidatorName = Name,
                Message = ok ? "Certificado válido" : "Certificado expirado/revogado",
                ShouldSkipNext = !ok // exemplo: se cert falhar, poderíamos pular próximo (configurável pela pipeline)
            });
        }
    }
}
