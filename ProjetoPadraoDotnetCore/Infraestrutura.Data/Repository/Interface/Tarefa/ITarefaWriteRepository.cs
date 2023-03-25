using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Tarefa;

public interface ITarefaWriteRepository : IBaseWriteRepository<Entity.Tarefa>
{
    public void DeleteRangeTarefas(List<Entity.Tarefa> list);
}