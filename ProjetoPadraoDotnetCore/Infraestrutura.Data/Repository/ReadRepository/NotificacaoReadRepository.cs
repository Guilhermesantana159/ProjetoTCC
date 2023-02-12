using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Notificacao;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class NotificacaoReadRepository : BaseReadRepository<Notificacao>,INotificacaoReadRepository
{
    public NotificacaoReadRepository(Context context) : base(context)
    {
    }
}