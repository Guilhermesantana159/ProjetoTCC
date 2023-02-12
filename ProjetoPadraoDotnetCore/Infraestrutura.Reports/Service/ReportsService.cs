using System.Data;
using AspNetCore.Reporting;
using Infraestrutura.Enum;

namespace Infraestrutura.Reports.Service;

public class ReportsService : IReportsService
{
    /// <summary>
    /// Gerar o relatorio
    /// </summary>
    /// <param name="arquivo"></param>
    /// <param name="listDataTable"></param>
    /// <param name="reportParams"></param>
    /// <param name="tipo"></param>
    /// <returns></returns>
    public Stream GerarRelatorio(string arquivo,List<DataTable> listDataTable,Dictionary<string,string> reportParams,ETipoArquivo tipo)
    {
        try
        {
            string currentDir = Environment.CurrentDirectory;
            FileInfo file = new FileInfo(currentDir + @"\Reports\" + arquivo);
            
            var localReport = new LocalReport(file.FullName);
            ReportResult result;

            var index = 1;
            foreach (var data in listDataTable)
            {
                localReport.AddDataSource($"DataSet{index.ToString()}", data);
                index++;
            }
            
            
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            
            if (tipo == ETipoArquivo.Excel)
                result = localReport.Execute(RenderType.Excel, parameters: reportParams);
            else if (tipo == ETipoArquivo.Word)
                result = localReport.Execute(RenderType.Word, parameters: reportParams);
            else
                result = localReport.Execute(RenderType.Pdf, parameters: reportParams);

            return new MemoryStream(result.MainStream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}