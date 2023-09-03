using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Modulo;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class ModuloReadRepository : BaseReadRepository<Modulo>,IModuloReadRepository
{
    public ModuloReadRepository(Context context) : base(context)
    {
        
    }

    public IQueryable<Modulo> GetWithInclude()
    {
        return Context.Modulo
            .AsNoTracking()
            .Include(x => x.LSubModulo)
            .ThenInclude(x => x.LMenus);
    }
}