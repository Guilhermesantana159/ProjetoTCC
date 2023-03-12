using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Atividade_Tarefa;

namespace Infraestrutura.Repository.ReadRepository;

public class AtividadeTarefaReadRepository : BaseReadRepository<AtividadeTarefa>,IAtividadeTarefaReadRepository
{
    private Context _context;
    public AtividadeTarefaReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}