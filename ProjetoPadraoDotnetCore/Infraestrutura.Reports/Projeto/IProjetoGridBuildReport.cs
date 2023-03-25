using Infraestrutura.Enum;
using Infraestrutura.Reports.Projeto.Obj;

namespace Infraestrutura.Reports.Projeto;

public interface IProjetoGridBuildReport
{
    public Stream GerarRelatorioGridProjeto(ETipoArquivo tipo, List<ProjetoGridReportObj> listUsuario);
}