using ValidationResult = Aplication.Utils.Objeto.ValidationResult;

namespace Aplication.Validators.Utils;

public interface IUtilsValidator
{
    public ValidationResult ValidarCep(string cep);
}