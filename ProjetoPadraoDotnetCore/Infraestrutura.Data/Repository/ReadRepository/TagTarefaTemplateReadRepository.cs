using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.TagTarefa;
using Infraestrutura.Repository.Interface.TagTarefaTemplate;

namespace Infraestrutura.Repository.ReadRepository;

public class TagTarefaTemplateReadRepository : BaseReadRepository<TagTarefaTemplate>,ITagTarefaTemplateReadRepository
{
    public TagTarefaTemplateReadRepository(Context context) : base(context)
    {
    }
}