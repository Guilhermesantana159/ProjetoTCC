using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Tarefa;

namespace Infraestrutura.Repository.ReadRepository;

public class TarefaReadRepository : BaseReadRepository<Tarefa>,ITarefaReadRepository
{
    private Context _context;
    public TarefaReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}