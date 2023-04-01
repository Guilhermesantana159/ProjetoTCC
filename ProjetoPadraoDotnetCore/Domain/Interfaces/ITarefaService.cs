using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface ITarefaService
{
    public Tarefa CadastrarComRetorno(Tarefa tarefa);
    public void DeleteRangeTarefas(List<Tarefa> tarefa);
    public IQueryable<Tarefa> GetTarefaByUsuario(List<int> atividadesId);
    public IQueryable<TarefaUsuario> GetTarefaUsuario(List<int> atividadesId);
}