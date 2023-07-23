using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.TagTarefa;
using Infraestrutura.Repository.Interface.TagTarefaTemplate;

namespace Infraestrutura.Repository.WriteRepository;

public class TagTarefaTemplateWriteRepository : BaseWriteRepository<TagTarefaTemplate>, ITagTarefaTemplateWriteRepository
{
    private Context _context;
    public TagTarefaTemplateWriteRepository(Context context) : base(context)
    {
        _context = context;
    }
}