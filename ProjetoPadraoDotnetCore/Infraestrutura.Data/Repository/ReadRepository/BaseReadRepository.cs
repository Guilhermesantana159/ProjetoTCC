using Infraestrutura.DataBaseContext;
using Infraestrutura.Repository.Interface.Base;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repository.ReadRepository;

public class BaseReadRepository <T> : IBaseReadRepository<T> where T : class
{
    public Context Context;

    public BaseReadRepository(Context context)
    {
        Context = context;
    }

    public T? GetById(int id)
    {
        return Context.Set<T>().Find(id);
    }

    public IQueryable<T> GetAll()
    {
        
        return Context.Set<T>()
            .AsNoTracking()
            .AsQueryable();
    }

    public void Dispose() {}
}