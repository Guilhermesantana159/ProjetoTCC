using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ContatoChat;

namespace Infraestrutura.Repository.WriteRepository;

public class ContatoChatWriteRepository : BaseWriteRepository<ContatoChat>, IContatoChatWriteRepository
{
    public ContatoChatWriteRepository(Context context) : base(context)
    {
    }
    
}