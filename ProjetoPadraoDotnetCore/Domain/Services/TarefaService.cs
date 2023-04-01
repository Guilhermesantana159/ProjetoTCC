using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Tarefa;

namespace Domain.Services;

public class TarefaService : ITarefaService
{
    protected readonly ITarefaReadRepository ReadRepository;
    protected readonly ITarefaWriteRepository WriteRepository;

    public TarefaService(ITarefaReadRepository readRepository, ITarefaWriteRepository writeRepository)
    {
        ReadRepository = readRepository;
        WriteRepository = writeRepository;
    }

    public Tarefa CadastrarComRetorno(Tarefa tarefa)
    {
        return  WriteRepository.AddWithReturn(tarefa);
    }
    
    public void DeleteRangeTarefas(List<Tarefa> tarefa)
    { 
        WriteRepository.DeleteRangeTarefas(tarefa);
    }
    
    public IQueryable<Tarefa> GetTarefaByUsuario(List<int> atividadesId)
    {
        return ReadRepository
            .GetAllWithInclude()
            .Where(x => atividadesId.Contains(x.IdAtividade));
    }
    
    public IQueryable<TarefaUsuario> GetTarefaUsuario(List<int> atividadesId)
    {
        return ReadRepository
            .GetAllTarefaUsuarioWithInclude()
            .Where(x => x.Tarefa != null && atividadesId.Contains(x.Tarefa.IdAtividade));
    }
}