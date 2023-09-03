using Infraestrutura.Enum;
using Infraestrutura.Reports.Projeto.Objeto;

namespace Infraestrutura.Reports.Projeto;

public interface IProjetoGridBuildReport
{
    public Stream GerarRelatorioGridProjeto(ETipoArquivo tipo, List<ProjetoGridReportObj> listUsuario);
}