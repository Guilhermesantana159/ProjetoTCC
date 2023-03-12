using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Projeto;

namespace Infraestrutura.Repository.ReadRepository;

public class ProjetoReadRepository : BaseReadRepository<Projeto>,IProjetoReadRepository
{
    private Context _context;
    public ProjetoReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}