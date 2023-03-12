using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Projeto_Atividade;

namespace Infraestrutura.Repository.ReadRepository;

public class ProjetoAtividadeReadRepository : BaseReadRepository<ProjetoAtividade>,IProjetoAtividadeReadRepository
{
    private Context _context;
    public ProjetoAtividadeReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}