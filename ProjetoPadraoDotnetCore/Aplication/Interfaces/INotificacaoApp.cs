using Aplication.Models.Request.Notificao;
using Infraestrutura.Entity;

namespace Aplication.Interfaces;

public interface INotificacaoApp
{
    public List<Notificacao> GetNotificaçõesByUser(int id);
    public void NotificacaoLida(NotificacaolidaRequest request);
}