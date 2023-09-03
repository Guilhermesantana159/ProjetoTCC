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
            var tarefaUsuario = _context.TarefaUsuario
                .AsQueryable()
                .Where(x => x.IdTarefa == item.IdTarefa);

            if (tarefaUsuario.Any())
            {
                _context.TarefaUsuario
                    .RemoveRange(tarefaUsuario);
            
                _context.SaveChanges();
            }

            var comentarioTarefa =  _context.ComentarioTarefa
                .AsQueryable()
                .Where(x => x.IdTarefa == item.IdTarefa);
        
            if (comentarioTarefa.Any())
            {
                _context.ComentarioTarefa
                    .RemoveRange(comentarioTarefa);
            
                _context.SaveChanges();
            }

            var movimentacaoTarefa = _context.MovimentacaoTarefa
                .AsQueryable()
                .Where(x => x.IdTarefa == item.IdTarefa);
        
            if (movimentacaoTarefa.Any())
            {
                _context.MovimentacaoTarefa
                    .RemoveRange(movimentacaoTarefa);
            
                _context.SaveChanges();
            }

            var tagTarefa = _context.TagTarefa
                .AsQueryable()
                .Where(x => x.IdTarefa == item.IdTarefa);
        
            if (tagTarefa.Any())
            {
                _context.TagTarefa
                    .RemoveRange(tagTarefa);
            
                _context.SaveChanges();
            }
            
            _context.Tarefa.Remove(_context.Tarefa
                .Find(item.IdTarefa) ?? item);
            
            _context.ChangeTracker.Clear();
            _context.SaveChanges();
        }
    }
    
    public void DeleteRangeTarefasUsuario(List<TarefaUsuario> tarefa)
    {
        _context.ChangeTracker.Clear();
        _context.TarefaUsuario
            .RemoveRange(tarefa);
        
        _context.SaveChanges();
    }

    public void AddTarefaUsuario(List<TarefaUsuario> tarefa)
    {
        _context.TarefaUsuario
            .AddRange(tarefa);
        
        _context.SaveChanges();    
    }
    
    public void DeleteTarefaWithIncludes(Tarefa tarefa)
    {
        var tarefaUsuario = _context.TarefaUsuario
            .AsQueryable()
            .Where(x => x.IdTarefa == tarefa.IdTarefa).ToList();

        if (tarefaUsuario.Any())
        {
            _context.TarefaUsuario
                .RemoveRange(tarefaUsuario);
            
            _context.SaveChanges();
        }

        var comentarioTarefa =  _context.ComentarioTarefa
                .AsQueryable()
                .Where(x => x.IdTarefa == tarefa.IdTarefa).ToList();
        
        if (comentarioTarefa.Any())
        {
            _context.ComentarioTarefa
                .RemoveRange(comentarioTarefa);
            
            _context.SaveChanges();
        }

        var movimentacaoTarefa = _context.MovimentacaoTarefa
                .AsQueryable()
                .Where(x => x.IdTarefa == tarefa.IdTarefa).ToList();
        
        if (movimentacaoTarefa.Any())
        {
            _context.MovimentacaoTarefa
                .RemoveRange(movimentacaoTarefa);
            
            _context.SaveChanges();
        }

        var tagTarefa = _context.TagTarefa
                .AsQueryable()
                .Where(x => x.IdTarefa == tarefa.IdTarefa).ToList();
        
        if (tagTarefa.Any())
        {
            _context.TagTarefa
                .RemoveRange(tagTarefa);
            
            _context.SaveChanges();
        }
        
        _context.ChangeTracker.Clear();
        _context.Tarefa.Remove(_context.Tarefa.Find(tarefa.IdTarefa) ?? tarefa);
        _context.SaveChanges();    
    }
}