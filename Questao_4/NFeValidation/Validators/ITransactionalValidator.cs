namespace NFeValidation.Validators
{
    /// <summary>
    /// Implementado por validadores que realizam mudanças persistentes e suportam rollback.
    /// </summary>
    public interface ITransactionalValidator : IValidator
    {
        /// <summary>
        /// Reverte a ação realizada por este validador.
        /// </summary>
        void Rollback();
    }
}
