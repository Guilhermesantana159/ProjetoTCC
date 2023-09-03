using Infraestrutura.DataBaseContext;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.CategoriaTemplate;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class CategoriaTemplateReadRepository : BaseReadRepository<CategoriaTemplate>,ICategoriaTemplateReadRepository
{
    protected readonly Context _context;
    public CategoriaTemplateReadRepository(Context context) : base(context)
    {
        _context = context;
    }
}