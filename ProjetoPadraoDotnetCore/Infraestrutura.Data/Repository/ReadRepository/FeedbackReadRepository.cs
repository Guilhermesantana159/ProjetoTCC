using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Feedback;

namespace Infraestrutura.Repository.ReadRepository;

public class FeedbackReadRepository : BaseReadRepository<Feedback>,IFeedbackReadRepository
{
    private Context _context;
    public FeedbackReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}