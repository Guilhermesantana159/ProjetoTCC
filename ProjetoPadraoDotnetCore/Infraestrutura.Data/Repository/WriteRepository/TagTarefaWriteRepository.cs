using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.TagTarefa;

namespace Infraestrutura.Repository.WriteRepository;

public class TagTarefaWriteRepository : BaseWriteRepository<TagTarefa>, ITagTarefaWriteRepository
{
    private Context _context;
    public TagTarefaWriteRepository(Context context) : base(context)
    {
        _context = context;
    }
}