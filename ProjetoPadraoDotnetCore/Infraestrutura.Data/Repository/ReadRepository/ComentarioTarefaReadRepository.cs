using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ComentarioTarefa;

namespace Infraestrutura.Repository.ReadRepository;

public class ComentarioTarefaReadRepository : BaseReadRepository<ComentarioTarefa>,IComentarioTarefaReadRepository
{
    public ComentarioTarefaReadRepository(Context context) : base(context)
    {
    }
}