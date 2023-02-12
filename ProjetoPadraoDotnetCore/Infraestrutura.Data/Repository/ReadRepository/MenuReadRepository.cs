using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Menu;

namespace Infraestrutura.Repository.ReadRepository;

public class MenuReadRepository : BaseReadRepository<Menu>,IMenuReadRepository
{
    public MenuReadRepository(Context context) : base(context)
    {
    }
}