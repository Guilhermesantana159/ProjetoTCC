using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Tarefa;

public interface ITarefaWriteRepository : IBaseWriteRepository<Entity.Tarefa>
{
    public void DeleteRangeTarefas(List<Entity.Tarefa> list);
    public void DeleteRangeTarefasUsuario(List<TarefaUsuario> tarefa);
    public void AddTarefaUsuario(List<TarefaUsuario> tarefa);
    public void DeleteTarefaWithIncludes(Entity.Tarefa tarefa);

}