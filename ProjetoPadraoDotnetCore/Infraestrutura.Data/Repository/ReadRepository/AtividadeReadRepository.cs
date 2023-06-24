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

    public Atividade? GetByIdWithInclude(int idAtividade)
    {
        return _context.Atividade
            .AsQueryable()
            .Include(x => x.ProjetoFk)
            .ThenInclude(x => x.Usuario)
            .Include(x => x.Tarefas)
            .ThenInclude(x => x.MovimentacaoTarefa)
            .FirstOrDefault(x => x.IdAtividade == idAtividade);
    }

    public List<Atividade> GetByIdProjeto(int idProjeto)
    {
        return _context.Atividade
            .AsQueryable()
            .Include(x => x.Tarefas)
            .Where(x => x.IdProjeto == idProjeto)
            .ToList();
    }
}