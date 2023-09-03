using Aplication.Models.Request.ModuloMenu;
using Aplication.Utils.Objeto;

namespace Aplication.Validators.EstruturaMenu;

public class EstruturaMenuValidator : IEstruturaMenuValidator
{
    public ValidationResult ValidaçãoIntegraçãoSubModulo(SubModuloRequest request)
    {
        var validation = new ValidationResult();
        
        if(string.IsNullOrEmpty(request.Icone))
            validation.LErrors.Add("Campo Icone é obrigatório!");
        if(string.IsNullOrEmpty(request.Nome))
            validation.LErrors.Add("Campo nome é obrigatório!");
        if(request.IdModulo == null)
            validation.LErrors.Add("Campo IdModulo é obrigatório!");

        return validation;    
    }
    
    public ValidationResult ValidaçãoIntegraçãoMenu(MenuRequest request)
    {
        var validation = new ValidationResult();
        
        if(string.IsNullOrEmpty(request.Link))
            validation.LErrors.Add("Campo Link é obrigatório!");
        if(string.IsNullOrEmpty(request.Nome))
            validation.LErrors.Add("Campo nome é obrigatório!");
        if(request.IdSubModulo == 0)
            validation.LErrors.Add("Campo IdSubModulo é obrigatório!");

        return validation;    
    }

    public ValidationResult ValidaçãoIntegraçãoModulo(ModuloRequest request)
    {
        var validation = new ValidationResult();
        
        if(string.IsNullOrEmpty(request.Nome))
            validation.LErrors.Add("Campo Nome é obrigatório!");

        return validation;        
    }
}