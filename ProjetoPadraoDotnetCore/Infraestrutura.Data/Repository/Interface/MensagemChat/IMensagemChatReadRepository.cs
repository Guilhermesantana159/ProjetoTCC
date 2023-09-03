using Infraestrutura.Repository.Interface.Base;
namespace Infraestrutura.Repository.Interface.MensagemChat;

public interface IMensagemChatReadRepository : IBaseReadRepository<Entity.MensagemChat>
{
    public IQueryable<Entity.MensagemChat> GetMensagensWithInclude();
}