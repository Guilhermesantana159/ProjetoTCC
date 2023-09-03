using Infraestrutura.Enum;
using Infraestrutura.Reports.Usuario.Objeto;

namespace Infraestrutura.Reports.Usuario;

public interface IUsuarioGridBuildReport
{
    public Stream GerarRelatorioGridUsuario(ETipoArquivo tipo,List<UsuarioGridReportObj> listUsuario);
}