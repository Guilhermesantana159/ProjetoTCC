using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ContatoMensagem;

namespace Infraestrutura.Repository.WriteRepository;

public class ContatoMensagemWriteRepository : BaseWriteRepository<ContatoMensagem>, IContatoMensagemWriteRepository
{
    public ContatoMensagemWriteRepository(Context context) : base(context)
    {
    }
}