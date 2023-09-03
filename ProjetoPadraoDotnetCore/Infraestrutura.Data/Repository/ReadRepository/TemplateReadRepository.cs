using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Template;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class TemplateReadRepository : BaseReadRepository<Template>,ITemplateReadRepository
{
    protected readonly Context _context;
    public TemplateReadRepository(Context context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Template> GetWithInclude()
    {
       return _context.Template
            .Include(x => x.CategoriaTemplate)
            .Include(x => x.LAtividadesTemplate)
            .ThenInclude(x => x.LTarefaTemplate)
            .ThenInclude(x => x.TagTarefaTemplate);

    }

    public IQueryable<Template> GetWithUserInclude()
    {
        return _context.Template
            .Include(x => x.CategoriaTemplate)
            .Include(x => x.UsuarioCadastro);
    }

}