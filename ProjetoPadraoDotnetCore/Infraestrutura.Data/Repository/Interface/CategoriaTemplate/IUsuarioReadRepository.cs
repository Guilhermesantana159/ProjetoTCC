using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Usuario;

public interface ICategoriaTemplateReadRepository : IBaseReadRepository<Entity.CategoriaTemplate>
{
    public Entity.Usuario GetByIdWithInclude(int id);
    public IQueryable<Entity.Usuario>? GetTarefaUsuarioWithInclude();

}