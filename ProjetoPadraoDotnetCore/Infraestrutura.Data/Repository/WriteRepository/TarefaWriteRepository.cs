using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Tarefa;

namespace Infraestrutura.Repository.WriteRepository;

public class TarefaWriteRepository : BaseWriteRepository<Tarefa>, ITarefaWriteRepository
{
    private Context _context;
    public TarefaWriteRepository(Context context) : base(context)
    {
        _context = context;
    }

    public void DeleteRangeTarefas(List<Tarefa> list)
    {
        foreach (var item in list)
        {
            _context.TarefaUsuario
                .RemoveRange(_context.TarefaUsuario
                    .AsQueryable()
                    .Where(x => x.IdTarefa == item.IdTarefa));
        }
        
        _context.Tarefa.RemoveRange(list);
        
        _context.SaveChanges();
    }
}