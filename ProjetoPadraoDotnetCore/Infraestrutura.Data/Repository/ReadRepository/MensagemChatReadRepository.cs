using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.MensagemChat;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class MensagemChatReadRepository : BaseReadRepository<MensagemChat>,IMensagemChatReadRepository
{
    private Context _context;
    public MensagemChatReadRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<MensagemChat> GetMensagensWithInclude()
    {
        return Context.MensagemChat
            .Include(x => x.ContatoRecebeChat)
            .ThenInclude(x => x.UsuarioContato);
    }
}