using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ContatoMensagem;

namespace Infraestrutura.Repository.ReadRepository;

public class ContatoMensagemReadRepository : BaseReadRepository<ContatoMensagem>,IContatoMensagemReadRepository
{
    public ContatoMensagemReadRepository(Context context) : base(context)
    {
    }
}