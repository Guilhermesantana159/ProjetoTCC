using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.AtvidadeTemplate;

namespace Infraestrutura.Repository.ReadRepository;

public class AtividadeTemplateReadRepository : BaseReadRepository<AtividadeTemplate>,IAtividadeTemplateReadRepository
{
    protected readonly Context _context;
    public AtividadeTemplateReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}