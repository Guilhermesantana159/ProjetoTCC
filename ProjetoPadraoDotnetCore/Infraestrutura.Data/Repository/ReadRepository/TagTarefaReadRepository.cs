using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.TagTarefa;

namespace Infraestrutura.Repository.ReadRepository;

public class TagTarefaReadRepository : BaseReadRepository<TagTarefa>,ITagTarefaReadRepository
{
    public TagTarefaReadRepository(Context context) : base(context)
    {
    }
}