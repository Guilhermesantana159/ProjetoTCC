using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Usuario;

namespace Infraestrutura.Repository.WriteRepository;

public class UsuarioWriteRepository : BaseWriteRepository<Usuario>, IUsuarioWriteRepository
{
    public UsuarioWriteRepository(Context context) : base(context)
    {
    }
}