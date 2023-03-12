using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Projeto_Atividade;

namespace Infraestrutura.Repository.WriteRepository;

public class ProjetoAtividadeWriteRepository : BaseWriteRepository<ProjetoAtividade>, IProjetoAtividadeWriteRepository
{
    public ProjetoAtividadeWriteRepository(Context context) : base(context)
    {
    }
}