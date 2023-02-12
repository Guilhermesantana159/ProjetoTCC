using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Notificacao;

namespace Infraestrutura.Repository.WriteRepository;

public class NotificacaoWriteRepository : BaseWriteRepository<Notificacao>, INotificacaoWriteRepository
{
    public NotificacaoWriteRepository(Context context) : base(context)
    {
    }
}