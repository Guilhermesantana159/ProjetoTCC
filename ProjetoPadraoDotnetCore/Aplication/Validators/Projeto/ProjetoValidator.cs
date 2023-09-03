using Aplication.Models.Request.Projeto;
using Aplication.Utils.Objeto;

namespace Aplication.Validators.Projeto;

public class ProjetoValidator : IProjetoValidator
{
    public ProjetoValidator()
    {
        
    }
    public ValidationResult ValidacaoCadastro(ProjetoRequest request)
    {
        var validation = new ValidationResult();

        if (string.IsNullOrEmpty(request.Titulo))
            validation.LErrors.Add("Campo titulo é obrigatório!");
        if(request.Atividade != null && !request.Atividade.Any())
            validation.LErrors.Add("Cadastre pelo menos uma atividade para o projeto!");

        return validation;    
    }
}