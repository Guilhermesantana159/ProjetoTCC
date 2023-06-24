using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.MovimentacaoTarefa;

namespace Infraestrutura.Repository.WriteRepository;

public class MovimentacaoTarefaWriteRepository : BaseWriteRepository<MovimentacaoTarefa>, IMovimentacaoTarefaWriteRepository
{
    private Context _context;
    public MovimentacaoTarefaWriteRepository(Context context) : base(context)
    {
        _context = context;
    }
}