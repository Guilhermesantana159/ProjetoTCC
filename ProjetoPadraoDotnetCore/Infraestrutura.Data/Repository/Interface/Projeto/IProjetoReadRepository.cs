using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Projeto;

public interface IProjetoReadRepository : IBaseReadRepository<Entity.Projeto>
{
    public Entity.Projeto GetByIdWithInclude(int id);
}