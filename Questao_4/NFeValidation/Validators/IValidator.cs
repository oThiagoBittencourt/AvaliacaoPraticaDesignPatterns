using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;

namespace NFeValidation.Validators
{
    /// <summary>
    /// Contrato para validadores. Cada validador implementa ExecuteAsync, respeitando token de cancelamento.
    /// </summary>
    public interface IValidator
    {
        string Name { get; }
        Task<ValidationResult> ExecuteAsync(string nfeXml, CancellationToken cancellationToken);
    }
}
