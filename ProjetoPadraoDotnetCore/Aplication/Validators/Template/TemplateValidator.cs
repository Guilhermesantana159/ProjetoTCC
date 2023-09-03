using Aplication.Models.Request.Projeto;
using Aplication.Models.Request.Template;
using Aplication.Utils.Objeto;
using Aplication.Validators.Projeto;

namespace Aplication.Validators.Template;

public class TemplateValidator : ITemplateValidator
{
    public TemplateValidator()
    {
        
    }
    public ValidationResult ValidacaoCadastro(TemplateRequest request)
    {
        var validation = new ValidationResult();
        
        if(string.IsNullOrEmpty(request.Titulo))
            validation.LErrors.Add("Titulo possui campo obrigatório!");
        if(!request.Escala.HasValue)
            validation.LErrors.Add("Escala possui campo obrigatório!");
        if(request.LAtividade == null)
            validation.LErrors.Add("Cadastre pelo menos um atividade!");
        if(!request.IdUsuarioCadastro.HasValue)
            validation.LErrors.Add("IdUsuarioCadastro possui campo obrigatório!");
        if(!request.QuantidadeTotal.HasValue)
            validation.LErrors.Add("QuantidadeTotal possui campo obrigatório!");

        if (request.LAtividade != null)
            foreach (var item in request.LAtividade)
            {
                if (string.IsNullOrEmpty(item.Titulo))
                    validation.LErrors.Add("Titulo da atividade é obrigatório!");
                
                if (!item.Posicao.HasValue)
                    validation.LErrors.Add("Posicao da atividade é obrigatório!");
                
                if (!item.TempoPrevisto.HasValue)
                    validation.LErrors.Add("TempoPrevisto da atividade é obrigatório!");

            }


        return validation;    
    }
}