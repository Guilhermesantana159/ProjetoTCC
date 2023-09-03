using Aplication.Models.Request.Projeto;
using Aplication.Models.Request.Template;
using Aplication.Utils.Objeto;

namespace Aplication.Validators.Template;

public interface ITemplateValidator
{
    public ValidationResult ValidacaoCadastro(TemplateRequest request);
}