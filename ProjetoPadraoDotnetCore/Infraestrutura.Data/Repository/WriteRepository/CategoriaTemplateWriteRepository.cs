using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.CategoriaTemplate;

namespace Infraestrutura.Repository.WriteRepository;

public class CategoriaTemplateWriteRepository : BaseWriteRepository<CategoriaTemplate>, ICategoriaTemplateWriteRepository
{
    public CategoriaTemplateWriteRepository(Context context) : base(context)
    {
    }
    
}