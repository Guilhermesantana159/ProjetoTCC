using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ComentarioTarefa;

namespace Infraestrutura.Repository.WriteRepository;

public class ComentarioTarefaWriteRepository : BaseWriteRepository<ComentarioTarefa>, IComentarioTarefaWriteRepository
{
    private Context _context;
    public ComentarioTarefaWriteRepository(Context context) : base(context)
    {
        _context = context;
    }
}