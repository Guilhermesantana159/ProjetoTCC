using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.MovimentacaoTarefa;

namespace Infraestrutura.Repository.ReadRepository;

public class MovimentacaoTarefaReadRepository : BaseReadRepository<MovimentacaoTarefa>,IMovimentacaoTarefaReadRepository
{
    public MovimentacaoTarefaReadRepository(Context context) : base(context)
    {
    }
}