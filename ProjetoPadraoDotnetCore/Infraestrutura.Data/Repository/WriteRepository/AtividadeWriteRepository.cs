using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Atividade;

namespace Infraestrutura.Repository.WriteRepository;

public class AtividadeWriteRepository : BaseWriteRepository<Atividade>, IAtividadeWriteRepository
{
    public AtividadeWriteRepository(Context context) : base(context)
    {
    }
}