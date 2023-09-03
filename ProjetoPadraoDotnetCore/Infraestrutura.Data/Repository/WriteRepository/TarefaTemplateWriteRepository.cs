using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.TarefaTemplate;

namespace Infraestrutura.Repository.WriteRepository;

public class TarefaTemplateWriteRepository : BaseWriteRepository<TarefaTemplate>, ITarefaTemplateWriteRepository
{
    public TarefaTemplateWriteRepository(Context context) : base(context)
    {
    }
    
}