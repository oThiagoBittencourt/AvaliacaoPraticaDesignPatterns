using System;
using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;

namespace NFeValidation.Validators
{
    /// <summary>
    /// Base com helper para executar a lógica com timeout/cancellation.
    /// Mantém o foco em SRP: execução/timeouts aqui, lógica real nos concretos.
    /// </summary>
    public abstract class ValidatorBase : IValidator
    {
        public abstract string Name { get; }

        // Cada validador implementa a lógica real em RunAsync (interna, sem timeout)
        protected abstract Task<ValidationResult> RunAsync(string nfeXml, CancellationToken token);

        public async Task<ValidationResult> ExecuteAsync(string nfeXml, CancellationToken cancellationToken)
        {
            // Apenas encaminha para RunAsync — cancela conforme token externo.
            try
            {
                return await RunAsync(nfeXml, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return new ValidationResult
                {
                    Success = false,
                    ValidatorName = Name,
                    Message = "Timeout / cancelado"
                };
            }
            catch (Exception ex)
            {
                return new ValidationResult
                {
                    Success = false,
                    ValidatorName = Name,
                    Message = $"Erro inesperado: {ex.Message}"
                };
            }
        }
    }
}
