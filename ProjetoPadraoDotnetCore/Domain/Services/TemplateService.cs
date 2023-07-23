using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.AtvidadeTemplate;
using Infraestrutura.Repository.Interface.CategoriaTemplate;
using Infraestrutura.Repository.Interface.TagTarefaTemplate;
using Infraestrutura.Repository.Interface.TarefaTemplate;
using Infraestrutura.Repository.Interface.Template;

namespace Domain.Services;

public class TemplateService : ITemplateService
{
    protected ITemplateReadRepository TemplateReadRepository;
    protected ITemplateWriteRepository TemplateWriteRepository;
    protected IAtividadeTemplateReadRepository AtividadeTemplateReadRepository;
    protected IAtividadeTemplateWriteRepository AtividadeTemplateWriteRepository;
    protected ITarefaTemplateReadRepository TarefaTemplateReadRepository;
    protected ITarefaTemplateWriteRepository TarefaTemplateWriteRepository;
    protected ICategoriaTemplateReadRepository CategoriaTemplateReadRepository;
    protected ICategoriaTemplateWriteRepository CategoriaTemplateWriteRepository;
    protected ITagTarefaTemplateWriteRepository TagTarefaTemplateWriteRepository;

    
    public TemplateService(ITemplateWriteRepository templateWriteRepository, ITemplateReadRepository templateReadRepository, IAtividadeTemplateReadRepository atividadeTemplateReadRepository, IAtividadeTemplateWriteRepository atividadeTemplateWriteRepository, ITarefaTemplateReadRepository tarefaTemplateReadRepository, ITarefaTemplateWriteRepository tarefaTemplateWriteRepository, ICategoriaTemplateReadRepository categoriaTemplateReadRepository, ICategoriaTemplateWriteRepository categoriaTemplateWriteRepository, ITagTarefaTemplateWriteRepository tagTarefaTemplateWriteRepository)
    {
        TemplateWriteRepository = templateWriteRepository;
        TemplateReadRepository = templateReadRepository;
        AtividadeTemplateReadRepository = atividadeTemplateReadRepository;
        AtividadeTemplateWriteRepository = atividadeTemplateWriteRepository;
        TarefaTemplateReadRepository = tarefaTemplateReadRepository;
        TarefaTemplateWriteRepository = tarefaTemplateWriteRepository;
        CategoriaTemplateReadRepository = categoriaTemplateReadRepository;
        CategoriaTemplateWriteRepository = categoriaTemplateWriteRepository;
        TagTarefaTemplateWriteRepository = tagTarefaTemplateWriteRepository;
    }


    public void ExcluirTarefasTemplate(List<TarefaTemplate> lTarefas)
    {
        foreach (var tag in lTarefas)
        {
            if (tag.TagTarefaTemplate.Any())
            {
                TagTarefaTemplateWriteRepository.DeleteRange(tag.TagTarefaTemplate.ToList());
            }
        }
        
        TarefaTemplateWriteRepository.DeleteRange(lTarefas);
    }
    
    public void ExcluirAtividadeTemplate(List<AtividadeTemplate> lAtividadeTemplate)
    {
        AtividadeTemplateWriteRepository.DeleteRange(lAtividadeTemplate);
    }
    
    public void ExcluirTemplate(int id)
    {
        TemplateWriteRepository.DeleteById(id);
    }
    
    public void CadastrarTemplate(Template entity)
    {
        TemplateWriteRepository.Add(entity);
    }
    
    public Template CadastrarTemplateComRetorno(Template entity)
    {
        return TemplateWriteRepository.AddWithReturn(entity);
    }
    
    public void EditarTemplate(Template entity)
    {
        TemplateWriteRepository.Update(entity);
    }
    public Template EditarTemplateComRetorno(Template entity)
    {
        return TemplateWriteRepository.UpdateWithReturn(entity);
    }
    
    public Template? ConsultarPorIdWithIncludes(int id)
    {
        return TemplateReadRepository.GetWithInclude().FirstOrDefault(x => x.IdTemplate == id);
    }
    
    public IQueryable<Template> GetAllTemplateGrid()
    {
        return TemplateReadRepository.GetWithUserInclude();
    }
    
    public IQueryable<Template> GetAllTemplate()
    {
        return TemplateReadRepository.GetAll();
    }
    
    public IQueryable<CategoriaTemplate> GetAllCategoria()
    {
        return CategoriaTemplateReadRepository.GetAll();
    }
    
    public CategoriaTemplate CadastrarCategoriaComRetorno(CategoriaTemplate categoria)
    {
        return CategoriaTemplateWriteRepository.AddWithReturn(categoria);
    }
    
    public void CadastrarCategoria(CategoriaTemplate categoria)
    {
        if(!categoria.IdCategoriaTemplate.HasValue)
            CategoriaTemplateWriteRepository.Add(categoria);
        else
            CategoriaTemplateWriteRepository.Update(categoria);
    }
    
    public CategoriaTemplate EditarCategoriaComRetorno(CategoriaTemplate categoria)
    {
        return CategoriaTemplateWriteRepository.AddWithReturn(categoria);
    }

    public void DeletarCategoria(int id)
    {
        CategoriaTemplateWriteRepository.DeleteById(id);
    }
    
    public void CadastrarRangeAtividade(List<AtividadeTemplate> atividade)
    {
        AtividadeTemplateWriteRepository.AddRange(atividade);
    }
}
