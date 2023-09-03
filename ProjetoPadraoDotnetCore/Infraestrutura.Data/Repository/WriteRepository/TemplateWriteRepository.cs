using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Template;

namespace Infraestrutura.Repository.WriteRepository;

public class TemplateWriteRepository : BaseWriteRepository<Template>, ITemplateWriteRepository
{
    public TemplateWriteRepository(Context context) : base(context)
    {
    }
    
}