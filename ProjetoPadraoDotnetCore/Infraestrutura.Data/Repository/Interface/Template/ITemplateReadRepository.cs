using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Template;

public interface ITemplateReadRepository : IBaseReadRepository<Entity.Template>
{
    public IQueryable<Entity.Template> GetWithInclude();
    public IQueryable<Entity.Template> GetWithUserInclude();
}