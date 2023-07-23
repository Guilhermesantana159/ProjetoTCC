using Infraestrutura.Repository.Interface.Base;

namespace Infraestrutura.Repository.Interface.ContatoChat;

public interface IContatoChatReadRepository : IBaseReadRepository<Entity.ContatoChat>
{
    public IQueryable<Entity.ContatoChat> GetAllWithIncludeQuery();
}