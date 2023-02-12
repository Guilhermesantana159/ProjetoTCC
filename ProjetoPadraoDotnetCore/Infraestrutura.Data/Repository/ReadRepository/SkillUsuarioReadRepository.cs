using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.SkillUsuario;

namespace Infraestrutura.Repository.ReadRepository;

public class SkillUsuarioReadRepository : BaseReadRepository<SkillUsuario>,ISkillUsuarioReadRepository
{
    public SkillUsuarioReadRepository(Context context) : base(context)
    {
    }
}