using Infraestrutura.Enum;

namespace Aplication.Models.Grid;

public class BaseGridRequest
{
    public int Take { get; set; }
    public int Page { get; set; }
    public OrderFilters? OrderFilters { get; set; }
    public List<QueryFilters>? QueryFilters { get; set; }
}

public class OrderFilters
{
    public string? Campo { get; set; }
    public EOperador Operador { get; set; }
}

public class QueryFilters
{
    public string? Value { get; set; }
    public string? Type { get; set; }
    public string? Field { get; set; }
    public EOperadorFilter EOperadorFilter { get; set; }
}
