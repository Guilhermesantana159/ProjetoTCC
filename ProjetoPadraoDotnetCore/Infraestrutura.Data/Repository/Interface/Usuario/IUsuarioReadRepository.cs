using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Usuario;

public interface IUsuarioReadRepository : IBaseReadRepository<Entity.Usuario>
{
    public Entity.Usuario GetByIdWithInclude(int id);
    public IQueryable<Entity.Usuario>? GetTarefaUsuarioWithInclude();
    public IQueryable<Entity.ContatoChat> GetAllContatoWithInclude();

}