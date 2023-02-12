using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Modulo;

namespace Infraestrutura.Repository.WriteRepository;

public class ModuloWriteRepository : BaseWriteRepository<Modulo>, IModuloWriteRepository
{
    public ModuloWriteRepository(Context context) : base(context)
    {
    }
}