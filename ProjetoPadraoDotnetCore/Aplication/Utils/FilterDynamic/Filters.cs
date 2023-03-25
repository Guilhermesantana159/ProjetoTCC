using System.Linq.Dynamic.Core;
using Aplication.Models.Grid;
using Infraestrutura.Enum;

namespace Aplication.Utils.FilterDynamic;

public static class Filters
{
    public static IQueryable<TSource> AplicarFiltrosDinamicos<TSource>(this IQueryable<TSource> source,
        List<QueryFilters>? queryFilter)
    {
        if (queryFilter == null || !queryFilter.Any())
            return source;

        foreach (var filter in queryFilter)
        {
            if (filter.Type == "string" && filter.EOperadorFilter == EOperadorFilter.Contains)
            {
                if (filter.Value != null && filter.Value != null)
                    source = source.Where(filter.Field + ".Contains(@0)",filter.Value);   
            }
            else if (filter.Type == "number" && filter.EOperadorFilter == EOperadorFilter.Contains)
            {
                if (filter.Value != null && filter.Value != null)
                    source = source.Where(filter.Field + ".ToString().Contains(@0)",filter.Value);
            }
            else if (filter.Type == "data" && filter.EOperadorFilter == EOperadorFilter.GreaterThen)
            {
                if (filter.Value != null && filter.Value != null)
                {
                    var date = DateTime.Parse(filter.Value);
                    source = source.Where(filter.Field + " >= @0", date);
                }
            }
            else if (filter.Type == "data" && filter.EOperadorFilter == EOperadorFilter.LessThen)
            {
                if (filter.Value != null && filter.Value != null)
                {
                    var date = DateTime.Parse(filter.Value);
                    source = source.Where(filter.Field + " <= @0", date);
                }
            }
            else if (filter.Type == "enum" && filter.EOperadorFilter == EOperadorFilter.Equals)
            {
                if (filter.Value != null && filter.Value != null)
                {
                    var numberEnum = Int32.Parse(filter.Value);
                    source = source.Where(filter.Field + " == @0",numberEnum);
                }
            }
            else
                source = null!;
        }
        
        return source;
    }    
}