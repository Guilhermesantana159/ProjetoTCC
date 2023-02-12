using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.SkillUsuario;

namespace Infraestrutura.Repository.WriteRepository;

public class SkillUsuarioWriteRepository : BaseWriteRepository<SkillUsuario>, ISkillUsuarioWriteRepository
{
    private Context _context;
    public SkillUsuarioWriteRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    public void RemoveSkillByUsuario(int id)
    {
        var lSkillUsuario = _context.SkillUsuario.AsQueryable().Where(x => x.IdUsuario == id);

        if (lSkillUsuario.Any())
        {
            _context.SkillUsuario.RemoveRange(lSkillUsuario);
            _context.SaveChanges();
        }
    }
}