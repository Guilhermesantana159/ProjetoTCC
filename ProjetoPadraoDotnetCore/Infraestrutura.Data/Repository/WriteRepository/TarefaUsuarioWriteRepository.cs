using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Tarefa_Usuario;

namespace Infraestrutura.Repository.WriteRepository;

public class TarefaUsuarioWriteRepository : BaseWriteRepository<TarefaUsuario>, ITarefaUsuarioWriteRepository
{
    public TarefaUsuarioWriteRepository(Context context) : base(context)
    {
    }
}