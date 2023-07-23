using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.TarefaTemplate;

namespace Infraestrutura.Repository.ReadRepository;

public class TarefaTemplateReadRepository : BaseReadRepository<TarefaTemplate>,ITarefaTemplateReadRepository
{
    protected readonly Context _context;
    public TarefaTemplateReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}