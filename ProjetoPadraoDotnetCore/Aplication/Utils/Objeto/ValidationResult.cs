namespace Aplication.Utils.Objeto;

public class ValidationResult
{
    public List<string> LErrors { get; }
    public string? MsgSucesso { get; set; }
    public object? Retorno { get; set; }

    public ValidationResult() 
    {
        LErrors = new List<string>();
    }

    public bool IsValid()
    {
        return !LErrors.Any();
    }
} 
