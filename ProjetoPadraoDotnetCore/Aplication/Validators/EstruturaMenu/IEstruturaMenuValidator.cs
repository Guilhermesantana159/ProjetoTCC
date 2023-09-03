using Aplication.Models.Request.ModuloMenu;
using ValidationResult = Aplication.Utils.Objeto.ValidationResult;

namespace Aplication.Validators.EstruturaMenu;

public interface IEstruturaMenuValidator
{
    public ValidationResult ValidaçãoIntegraçãoSubModulo(SubModuloRequest request);
    public ValidationResult ValidaçãoIntegraçãoMenu(MenuRequest request);
    public ValidationResult ValidaçãoIntegraçãoModulo(ModuloRequest request);
}