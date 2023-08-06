using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.Atividade;

public interface IAtividadeReadRepository : IBaseReadRepository<Entity.Atividade>
{
    public Entity.Atividade? GetByIdWithInclude(int idAtividade);
    public List<Entity.Atividade> GetByIdProjeto(int idProjeto);
    public IQueryable<Entity.Atividade> GetAllWithInclude();

}