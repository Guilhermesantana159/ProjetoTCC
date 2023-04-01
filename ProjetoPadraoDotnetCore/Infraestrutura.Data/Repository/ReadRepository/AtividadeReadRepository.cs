using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Atividade;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class AtividadeReadRepository : BaseReadRepository<Atividade>,IAtividadeReadRepository
{
    private Context _context;
    public AtividadeReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}