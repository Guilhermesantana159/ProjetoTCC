using Aplication.Models.Grid;
using Aplication.Models.Request.Projeto;
using Aplication.Utils.Obj;
using Infraestrutura.Reports.Projeto.Obj;

namespace Aplication.Interfaces;

public interface IProjetoApp
{
    public ValidationResult Cadastrar(ProjetoRequest request);
    public BaseGridResponse ConsultarGridProjeto(ProjetoGridRequest request);
    public ValidationResult MudarStatusProjeto(ProjetoStatusRequest request);
    public ValidationResult Deletar(ProjetoDeleteRequest request);
    public List<ProjetoGridReportObj> ConsultarRelatorioGridProjeto(ProjetoRelatorioGridRequest request);
}