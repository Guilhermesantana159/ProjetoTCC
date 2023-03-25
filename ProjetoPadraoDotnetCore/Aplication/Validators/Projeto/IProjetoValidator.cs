using Aplication.Models.Request.Projeto;
using Aplication.Utils.Obj;

namespace Aplication.Validators.Projeto;

public interface IProjetoValidator
{
    public ValidationResult ValidacaoCadastro(ProjetoRequest request);
}