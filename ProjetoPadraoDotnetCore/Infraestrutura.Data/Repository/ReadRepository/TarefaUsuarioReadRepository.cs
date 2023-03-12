using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Tarefa_Usuario;

namespace Infraestrutura.Repository.ReadRepository;

public class TarefaUsuarioReadRepository : BaseReadRepository<TarefaUsuario>,ITarefaUsuarioReadRepository
{
    private Context _context;
    public TarefaUsuarioReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}
