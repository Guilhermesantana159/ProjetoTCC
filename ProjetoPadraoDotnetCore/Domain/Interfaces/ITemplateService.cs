using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface ITemplateService
{
    public void ExcluirTarefasTemplate(List<TarefaTemplate> lTarefas);
    public void ExcluirAtividadeTemplate(List<AtividadeTemplate> lAtividadeTemplate);
    public void ExcluirTemplate(int id);
    public Template CadastrarTemplateComRetorno(Template entity);
    public void CadastrarTemplate(Template entity);
    public void EditarTemplate(Template entity);
    public Template EditarTemplateComRetorno(Template entity);
    public Template? ConsultarPorIdWithIncludes(int id);
    public IQueryable<Template> GetAllTemplateGrid();
    public IQueryable<Template> GetAllTemplate();
    public IQueryable<CategoriaTemplate> GetAllCategoria();
    public CategoriaTemplate CadastrarCategoriaComRetorno(CategoriaTemplate categoria);
    public void CadastrarCategoria(CategoriaTemplate categoria);
    public CategoriaTemplate EditarCategoriaComRetorno(CategoriaTemplate categoria);
    public void DeletarCategoria(int id);
    public void CadastrarRangeAtividade(List<AtividadeTemplate> atividade);
}