using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.SubModulo;

public interface ISubModuloReadRepository : IBaseReadRepository<Entity.SubModulo>
{
    public IQueryable<Entity.SubModulo> GetWithInclude();
}