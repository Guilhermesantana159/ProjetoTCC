using ValidationResult = Aplication.Utils.Obj.ValidationResult;

namespace Aplication.Validators.Utils;

public interface IUtilsValidator
{
    public ValidationResult ValidarCep(string cep);
}