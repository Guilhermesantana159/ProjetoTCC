using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Tarefa;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class TarefaReadRepository : BaseReadRepository<Tarefa>,ITarefaReadRepository
{
    private Context _context;
    public TarefaReadRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Tarefa> GetAllWithInclude()
    {
        return _context.Tarefa
            .AsQueryable()
            .Include(x => x.MovimentacaoTarefa)!
            .ThenInclude(x => x.Usuario)
            .Include(x => x.AtividadeFk)
            .ThenInclude(x => x.Tarefas)
            .Include(x => x.TagTarefa)
            .Include(x => x.ComentarioTarefa)!
            .ThenInclude(x => x.Usuario)
            .Include(x => x.TagTarefa)
            .Include(x => x.TarefaUsuario)!
            .ThenInclude(x => x.Usuario);
    }
    
    public Tarefa? GetByIdWithInclude(int idAtividade)
    {
        return _context.Tarefa!
            .AsQueryable()
            .Include(x => x.TarefaUsuario)!
            .ThenInclude(x => x.Usuario)
            .FirstOrDefault(x => x.IdTarefa == idAtividade);
    }
    
    public IQueryable<TarefaUsuario> GetAllTarefaUsuarioWithInclude()
    {
        return _context.TarefaUsuario
            .AsQueryable()
            .Include(x => x.Usuario)
            .Include(x => x.Tarefa)
            .ThenInclude(x => x!.AtividadeFk);
    }
}