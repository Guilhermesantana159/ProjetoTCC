using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Feedback;

namespace Infraestrutura.Repository.WriteRepository;

public class FeedbackWriteRepository : BaseWriteRepository<Feedback>, IFeedbackWriteRepository
{
    private Context _context;
    public FeedbackWriteRepository(Context context) : base(context)
    {
        _context = context;
    }
}