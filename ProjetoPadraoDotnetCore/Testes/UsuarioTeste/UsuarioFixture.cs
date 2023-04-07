using Aplication.Utils.Objeto;

namespace Tests.UsuarioTeste;

public class UsuarioFixture
{
    public ValidationResult RetornarInvalidoResult()
    {
        var result = new ValidationResult();
        result.LErrors.Add("Email já informado");
        
        return result;
    }
}