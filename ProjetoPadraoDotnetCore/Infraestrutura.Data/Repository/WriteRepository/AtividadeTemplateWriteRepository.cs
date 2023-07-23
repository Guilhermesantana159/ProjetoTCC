using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.AtvidadeTemplate;
using Infraestrutura.Repository.Interface.Template;

namespace Infraestrutura.Repository.WriteRepository;

public class AtividadeTemplateWriteRepository : BaseWriteRepository<AtividadeTemplate>, IAtividadeTemplateWriteRepository
{
    public AtividadeTemplateWriteRepository(Context context) : base(context)
    {
    }
    
}