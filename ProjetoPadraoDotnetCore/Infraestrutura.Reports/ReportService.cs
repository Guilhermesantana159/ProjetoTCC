using AspNetCore.Reporting;

namespace Infraestrutura.Reports;

public class ReportService
{
    public ReportService()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public MemoryStream GerarArquivo()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        
        var result = LocalReport.Execute(RenderType.Excel, parameters: reportParams);
        
        return new MemoryStream(result.MainStream);
    }
    
    public MemoryStream GerarDataTables()
    {

    }
}