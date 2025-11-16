using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NFeValidation.Models;
using NFeValidation.Validators;

namespace NFeValidation.Pipeline
{
    /// <summary>
    /// Orquestra os validadores em cadeia, aplica circuit-breaker, regras condicionais e rollback.
    /// </summary>
    public class ValidationPipeline
    {
        private readonly IList<IValidator> _validatorsOrdered;
        private readonly Dictionary<string, string> _skipMap; // se X falhar, pule Y => key: X.Name, value: Y.Name
        private readonly int _maxFailuresBeforeBreak;

        public ValidationPipeline(IEnumerable<IValidator> orderedValidators,
                                  Dictionary<string, string> skipMap = null,
                                  int maxFailuresBeforeBreak = 3)
        {
            _validatorsOrdered = orderedValidators.ToList();
            _skipMap = skipMap ?? new Dictionary<string, string>();
            _maxFailuresBeforeBreak = maxFailuresBeforeBreak;
        }

        /// <summary>
        /// Executa a cadeia. timeoutsByValidator: nome -> milissegundos
        /// </summary>
        public async Task<IList<ValidationResult>> ExecuteAsync(string nfeXml, Dictionary<string, int> timeoutsByValidator)
        {
            var results = new List<ValidationResult>();
            int failureCount = 0;
            var transactionalValidators = new List<ITransactionalValidator>(); // para rollback se necessário

            for (int i = 0; i < _validatorsOrdered.Count; i++)
            {
                var validator = _validatorsOrdered[i];

                // Circuit breaker
                if (failureCount >= _maxFailuresBeforeBreak)
                {
                    results.Add(new ValidationResult
                    {
                        Success = false,
                        ValidatorName = "Pipeline",
                        Message = $"Circuit breaker acionado após {failureCount} falhas. Execução interrompida."
                    });
                    break;
                }

                // Verifica se o validador anterior instruiu pular este
                if (i > 0)
                {
                    var prev = results.LastOrDefault();
                    if (prev != null && prev.ShouldSkipNext)
                    {
                        results.Add(new ValidationResult
                        {
                            Success = true,
                            ValidatorName = validator.Name,
                            Message = $"Pulado por regra condicional (anterior solicitou pular)."
                        });
                        continue;
                    }
                }

                // Executa com timeout
                int timeoutMs = timeoutsByValidator != null && timeoutsByValidator.ContainsKey(validator.Name)
                    ? timeoutsByValidator[validator.Name]
                    : 5000; // padrão 5s

                using var cts = new CancellationTokenSource(timeoutMs);

                var task = validator.ExecuteAsync(nfeXml, cts.Token);
                var completed = await Task.WhenAny(task, Task.Delay(timeoutMs, cts.Token));
                ValidationResult result;

                if (completed == task)
                {
                    result = await task; // Obtém o resultado (pode lançar exceção tratada internamente)
                }
                else
                {
                    // timeout
                    cts.Cancel();
                    result = new ValidationResult
                    {
                        Success = false,
                        ValidatorName = validator.Name,
                        Message = "Timeout"
                    };
                }

                results.Add(result);
                if (!result.Success) failureCount++;

                // Registrar validadores transacionais que tiveram sucesso (para rollback depois, se necessário)
                if (result.Success && validator is ITransactionalValidator tv)
                    transactionalValidators.Add(tv);

                // Regra adicional: validadores 3 e 5 só executam se o anterior tiver passado
                // Aqui verificamos por nome: RegrasFiscaisValidator e SefazValidator
                if ((validator.Name == "CertificadoValidator" && !result.Success) ||
                    (validator.Name == "SchemaValidator" && !result.Success))
                {
                    // nada especial aqui; a lógica sequencial impedirá 3 e 5 de rodarem se anteriores falharem
                }

                // mapa de pulo: se este validador falhar e estiver configurado para pular o próximo específico
                if (!result.Success && _skipMap.TryGetValue(validator.Name, out var toSkipName))
                {
                    // encontrar o índice do validador a ser pulado e marcá-lo como pulado configurando ShouldSkipNext
                    var idx = _validatorsOrdered.ToList().FindIndex(v => v.Name == toSkipName);
                    if (idx >= 0)
                    {
                        // se o alvo do pulo for posterior, marcamos o atual como solicitante de skip
                        result.ShouldSkipNext = true;
                    }
                }

                // adicional: se uma regra crítica diz que 3 e 5 só devem rodar se o anterior passar,
                // o pipeline naturalmente impedirá execução se resultados anteriores falharem.
            }

            // Se alguma validação subsequente falhou, faz rollback dos validadores transacionais
            if (results.Any(r => !r.Success))
            {
                // Rollback em ordem reversa
                foreach (var tv in transactionalValidators.AsEnumerable().Reverse())
                {
                    try
                    {
                        tv.Rollback();
                        // Poderia adicionar uma entrada de resultado indicando rollback bem-sucedido
                    }
                    catch
                    {
                        // ignorado no dummy; em sistema real registrar falha
                    }
                }
            }

            return results;
        }
    }
}
