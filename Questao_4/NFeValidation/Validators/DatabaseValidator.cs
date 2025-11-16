using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;
using System;

namespace NFeValidation.Validators
{
    /// <summary>
    /// Simula inserção em BD e suporta rollback se validações seguintes falharem.
    /// </summary>
    public class DatabaseValidator : ValidatorBase, ITransactionalValidator
    {
        public override string Name => "DatabaseValidator";
        private bool _inserted = false;
        private readonly string _fakeId = Guid.NewGuid().ToString();

        protected override Task<ValidationResult> RunAsync(string nfeXml, CancellationToken token)
        {
            // Dummy: verifica duplicidade e "insere" na base
            bool duplicate = false; // ajuste para testes

            if (duplicate)
            {
                return Task.FromResult(new ValidationResult
                {
                    Success = false,
                    ValidatorName = Name,
                    Message = "Duplicidade detectada"
                });
            }

            // Simula inserção (que precisará ser revertida se algo falhar depois)
            _inserted = true;
            return Task.FromResult(new ValidationResult
            {
                Success = true,
                ValidatorName = Name,
                Message = $"Registro inserido (id={_fakeId})"
            });
        }

        public void Rollback()
        {
            if (_inserted)
            {
                // Dummy: reverte a inserção
                _inserted = false;
                // log de rollback (no real, faria DELETE ou abortaria a transação)
            }
        }
    }
}
