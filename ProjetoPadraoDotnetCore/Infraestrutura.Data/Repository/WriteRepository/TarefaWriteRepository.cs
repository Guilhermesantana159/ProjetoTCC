using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Tarefa;

namespace Infraestrutura.Repository.WriteRepository;

public class TarefaWriteRepository : BaseWriteRepository<Tarefa>, ITarefaWriteRepository
{
    public TarefaWriteRepository(Context context) : base(context)
    {
    }
}