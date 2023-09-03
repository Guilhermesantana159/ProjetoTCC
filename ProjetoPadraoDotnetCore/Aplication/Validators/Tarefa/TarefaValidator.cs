using Aplication.Models.Request.Tarefa;
using Aplication.Utils.Objeto;

namespace Aplication.Validators.Tarefa;

public class TarefaValidator : ITarefaValidator
{
    public TarefaValidator()
    {
        
    }

    public ValidationResult ValidacaoCadastro(TarefaAdmRequest request)
    {
        var validation = new ValidationResult();
        
        if(string.IsNullOrEmpty(request.Descricao))
            validation.LErrors.Add("Campo titulo é obrigatório!");
        if(!request.IdAtividade.HasValue)
            validation.LErrors.Add("campo atividade obrigatório!");

        return validation;        
    }

    public ValidationResult ValidacaoCadastroComentario(ComentarioTarefaRequest request)
    {
        var validation = new ValidationResult();

        if(string.IsNullOrEmpty(request.Descricao))
            validation.LErrors.Add("Campo descrição é obrigatório!");
        if (!request.IdTarefa.HasValue)
            validation.LErrors.Add("Campo idTarefa é obrigatório!");
        if (!request.IdUsuario.HasValue)
            validation.LErrors.Add("Campo idUsuario é obrigatório!");


        return validation;    
    }
}