using Infraestrutura.Enum;

namespace Aplication.Models.Grid;

public class BaseReportGridRequest
{
    public OrderFilters? OrderFilters { get; set; }
    public List<QueryFilters>? QueryFilters { get; set; }
    public ETipoArquivo Tipo { get; set; }
}

