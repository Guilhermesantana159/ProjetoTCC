using System.Data;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Projeto.Objeto;
using Infraestrutura.Reports.Service;

namespace Infraestrutura.Reports.Projeto;

public class ProjetoGridBuildReport : IProjetoGridBuildReport
{
    private readonly string _relatorio = "ProjetoReport.rdlc";
    private IReportsService _service;
    public ProjetoGridBuildReport(IReportsService service)
    {
        _service = service;
    }
    
    public Stream GerarRelatorioGridProjeto(ETipoArquivo tipo,List<ProjetoGridReportObj> listUsuario)
    {
        var listDataTable = new List<DataTable>();
        var reportParams = new Dictionary<string, string>();
        var dt = new DataTable();
        
        //Header
        dt.Clear();
        dt.Columns.Add("IdProjeto", typeof(int));
        dt.Columns.Add("Titulo", typeof(string));
        dt.Columns.Add("DataInicial", typeof(string));
        dt.Columns.Add("DataFim", typeof(string));
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("Andamento", typeof(string));

        //Body
         foreach (var item in listUsuario)
        {
            dt.Rows.Add(item.IdProjeto,item.Titulo,item.DataInicial,
                item.DataFim,item.Status,item.Amdamento);
        }
        
        listDataTable.Add(dt);
        
        //Parametros do relatório
        //reportParams.Add("ImagemLogo",_configuration.GetSection("LogoUfsc:Logo").Value);

        return _service.GerarRelatorio(_relatorio,listDataTable,reportParams,tipo);

    }
}