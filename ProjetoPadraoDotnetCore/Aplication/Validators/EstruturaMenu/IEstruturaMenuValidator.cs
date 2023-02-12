using Aplication.Models.Request.ModuloMenu;
using ValidationResult = Aplication.Utils.Obj.ValidationResult;

namespace Aplication.Validators.EstruturaMenu;

public interface IEstruturaMenuValidator
{
    public ValidationResult ValidaçãoIntegraçãoModulo(ModuloRequest request);
    public ValidationResult ValidaçãoIntegraçãoMenu(MenuRequest request);
}