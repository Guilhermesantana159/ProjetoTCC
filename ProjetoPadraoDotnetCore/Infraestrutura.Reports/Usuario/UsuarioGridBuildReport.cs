using System.Data;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Service;
using Infraestrutura.Reports.Usuario.Obj;

namespace Infraestrutura.Reports.Usuario;

public class UsuarioGridBuildReport : IUsuarioGridBuildReport
{
    private readonly string _relatorio = "UsuarioReport.rdlc";
    private readonly IConfiguration _configuration;
    private IReportsService _service;
    public UsuarioGridBuildReport(IReportsService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }
    
    public Stream GerarRelatorioGridUsuario(ETipoArquivo tipo,List<UsuarioGridReportObj> listUsuario)
    {
        var listDataTable = new List<DataTable>();
        var reportParams = new Dictionary<string, string>();
        var dt = new DataTable();
        
        //Header
        dt.Clear();
        dt.Columns.Add("IdUsuario", typeof(int));
        dt.Columns.Add("Nome", typeof(string));
        dt.Columns.Add("Email", typeof(string));
        dt.Columns.Add("CPF", typeof(string));
        dt.Columns.Add("PerfilAdministrador", typeof(string));
        dt.Columns.Add("Dedicacao", typeof(string));
        dt.Columns.Add("DataNascimento", typeof(string));

        //Body
        foreach (var item in listUsuario)
        {
            dt.Rows.Add(item.IdUsuario, item.Nome,item.Email,item.Cpf,
                item.PerfilAdministrador ? "Sim" : "Não",item.Dedicacao + "%",item.DataNascimento == null ? "Não cadastrado" : item.DataNascimento.Value.ToString("MM/dd/yyyy"));
        }
        
        listDataTable.Add(dt);
        
        //Parametros do relatório
        //reportParams.Add("ImagemLogo",_configuration.GetSection("LogoUfsc:Logo").Value);

        return _service.GerarRelatorio(_relatorio,listDataTable,reportParams,tipo);

    }
}