namespace NFeValidation.Models
{
    public class ValidationResult
    {
        public bool Success { get; set; }
        public string ValidatorName { get; set; }
        public string Message { get; set; }
        public bool ShouldSkipNext { get; set; } = false; // permite regras condicionais: se X falhar, pule Y
    }
}
