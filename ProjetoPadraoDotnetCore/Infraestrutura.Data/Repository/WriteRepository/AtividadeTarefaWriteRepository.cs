using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Atividade_Tarefa;

namespace Infraestrutura.Repository.WriteRepository;

public class AtividadeTarefaWriteRepository : BaseWriteRepository<AtividadeTarefa>, IAtividadeTarefaWriteRepository
{
    public AtividadeTarefaWriteRepository(Context context) : base(context)
    {
    }
}