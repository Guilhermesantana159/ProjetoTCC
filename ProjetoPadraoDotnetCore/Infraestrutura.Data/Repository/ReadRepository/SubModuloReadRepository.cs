using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.SubModulo;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class SubModuloReadRepository : BaseReadRepository<SubModulo>,ISubModuloReadRepository
{
    public SubModuloReadRepository(Context context) : base(context)
    {
        
    }

    public IQueryable<SubModulo> GetWithInclude()
    {
        return Context.SubModulo                
            .AsNoTracking()
            .Include(x => x.LMenus);
    }
}