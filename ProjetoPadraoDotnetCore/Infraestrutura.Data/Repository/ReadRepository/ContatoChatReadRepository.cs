using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ContatoChat;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class ContatoChatReadRepository : BaseReadRepository<ContatoChat>,IContatoChatReadRepository
{
    protected readonly Context _context;
    public ContatoChatReadRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<ContatoChat> GetAllWithIncludeQuery()
    {
        return _context.ContatoChat
            .Include(x => x.UsuarioContato);
    }
}