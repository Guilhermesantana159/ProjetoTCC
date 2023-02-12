using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Profissao;

namespace Infraestrutura.Repository.ReadRepository;

public class ProfissaoReadRepository : BaseReadRepository<Profissao>,IProfissaoReadRepository
{
    public ProfissaoReadRepository(Context context) : base(context)
    {
    }
}