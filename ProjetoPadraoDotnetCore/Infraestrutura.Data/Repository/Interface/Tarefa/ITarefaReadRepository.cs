using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Tarefa;

public interface ITarefaReadRepository : IBaseReadRepository<Entity.Tarefa>
{
    public IQueryable<Entity.Tarefa> GetAllWithInclude();
    public IQueryable<TarefaUsuario> GetAllTarefaUsuarioWithInclude();

}