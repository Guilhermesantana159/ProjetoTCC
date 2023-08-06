using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.MensagemChat;

namespace Infraestrutura.Repository.WriteRepository;

public class MensagemChatWriteRepository : BaseWriteRepository<MensagemChat>, IMensagemChatWriteRepository
{
    private Context _context;
    public MensagemChatWriteRepository(Context context) : base(context)
    {
        _context = context;
    }
}