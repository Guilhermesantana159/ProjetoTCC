using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Profissao;

namespace Infraestrutura.Repository.WriteRepository;

public class ProfissaoWriteRepository : BaseWriteRepository<Profissao>, IProfissaoWriteRepository
{
    public ProfissaoWriteRepository(Context context) : base(context)
    {
    }
}