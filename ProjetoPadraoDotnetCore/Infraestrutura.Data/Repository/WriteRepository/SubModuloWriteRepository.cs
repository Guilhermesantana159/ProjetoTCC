using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.SubModulo;

namespace Infraestrutura.Repository.WriteRepository;

public class SubModuloWriteRepository : BaseWriteRepository<SubModulo>, ISubModuloWriteRepository
{
    public SubModuloWriteRepository(Context context) : base(context)
    {
    }
}