using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Menu;

namespace Infraestrutura.Repository.WriteRepository;

public class MenuWriteRepository : BaseWriteRepository<Menu>, IMenuWriteRepository
{
    public MenuWriteRepository(Context context) : base(context)
    {
    }
}