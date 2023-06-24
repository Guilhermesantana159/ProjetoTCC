using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class UsuarioReadRepository : BaseReadRepository<Usuario>,IUsuarioReadRepository
{
    private Context _context;
    public UsuarioReadRepository(Context context) : base(context)
    {
        _context = context;
    }

    public Usuario GetByIdWithInclude(int id)
    {
        return _context.Usuario
                   .Include(x => x.LSkillUsuarios)
                   .FirstOrDefault(x => x.IdUsuario == id) ?? 
               throw new InvalidOperationException($"Usuário com Id {id} não encontrado!");
    }
    
    public IQueryable<Usuario> GetTarefaUsuarioWithInclude()
    {
        return _context.Usuario
            .Include(x => x.TarefaUsuario);
    }
}