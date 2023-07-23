using Aplication.Models.Grid;
using Aplication.Models.Request.Template;
using Aplication.Models.Response.Base;
using Aplication.Models.Response.Template;
using Aplication.Utils.Objeto;

namespace Aplication.Interfaces;

public interface ITemplateApp
{
  public ValidationResult IntegrarTemplate(TemplateRequest request);
  public BaseGridResponse ConsultarGridTemplate(TemplateRequestGridRequest request);
  public void DeletarTemplate(int id);
  public List<SelectBase> ConsultarCategorias();
  public ValidationResult IntegrarCategoria(CategoriaRequest request);
  public void DeletarCategoria(int id);
  public TemplateResponse ConsultarViaId(int id);
}