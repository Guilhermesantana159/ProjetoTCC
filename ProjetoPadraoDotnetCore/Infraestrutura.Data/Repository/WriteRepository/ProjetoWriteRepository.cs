using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Projeto;

namespace Infraestrutura.Repository.WriteRepository;

public class ProjetoWriteRepository : BaseWriteRepository<Projeto>, IProjetoWriteRepository
{
    public ProjetoWriteRepository(Context context) : base(context)
    {
    }
}