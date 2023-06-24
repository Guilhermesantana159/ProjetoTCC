using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Projeto;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class ProjetoReadRepository : BaseReadRepository<Projeto>,IProjetoReadRepository
{
    private Context _context;
    public ProjetoReadRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    public Projeto GetByIdWithInclude(int id)
    {
        return _context.Projeto
                   .Include(x => x.Atividades)
                   .ThenInclude(x => x.Tarefas)
                   .ThenInclude(x => x.TarefaUsuario)
                   .ThenInclude(x => x.Usuario)
                   .Include(x => x.Usuario)
                   .FirstOrDefault(x => x.IdProjeto == id) ?? throw new InvalidOperationException($"Projeto com Id: {id} não encontrado!");
    }
    
    public IQueryable<Projeto> GetAllWithIncludeQuery()
    {
        return _context.Projeto
            .Include(x => x.Atividades)
            .ThenInclude(x => x.Tarefas)
            .ThenInclude(x => x.TarefaUsuario)
            .Include(x => x.Usuario);
    }
}