using Aplication.Models.Request.Tarefa;
using Aplication.Utils.Objeto;

namespace Aplication.Validators.Tarefa;

public interface ITarefaValidator
{
    public ValidationResult ValidacaoCadastro(TarefaAdmRequest request);
    public ValidationResult ValidacaoCadastroComentario(ComentarioTarefaRequest request);
}