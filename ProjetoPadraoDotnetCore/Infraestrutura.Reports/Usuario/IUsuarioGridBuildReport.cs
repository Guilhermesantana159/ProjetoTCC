using Infraestrutura.Enum;
using Infraestrutura.Reports.Usuario.Obj;

namespace Infraestrutura.Reports.Usuario;

public interface IUsuarioGridBuildReport
{
    public Stream GerarRelatorioGridUsuario(ETipoArquivo tipo,List<UsuarioGridReportObj> listUsuario);
}