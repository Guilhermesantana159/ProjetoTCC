using System.Data;
using Infraestrutura.Enum;

namespace Infraestrutura.Reports.Service;

public interface IReportsService
{
    public Stream GerarRelatorio(string arquivo, List<DataTable> listDataTable,
        Dictionary<string, string> reportParams, ETipoArquivo tipo);
}