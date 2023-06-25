using Aplication.Models.Grid;
using Aplication.Models.Request.Projeto;
using Aplication.Models.Response.Projeto;
using Aplication.Models.Response.Tarefa;
using Aplication.Utils.Objeto;
using Infraestrutura.Reports.Projeto.Objeto;

namespace Aplication.Interfaces;

public interface IProjetoApp
{
    public ValidationResult Cadastrar(ProjetoRequest request);
    public ValidationResult Editar(ProjetoRequest request);
    public BaseGridResponse ConsultarGridProjeto(ProjetoGridRequest request);
    public ValidationResult MudarStatusProjeto(ProjetoStatusRequest request);
    public ValidationResult Deletar(ProjetoDeleteRequest request);
    public List<ProjetoGridReportObj> ConsultarRelatorioGridProjeto(ProjetoRelatorioGridRequest request);
    public ProjetoResponse GetById(int id);
    public void DeletarAtividade(int idAtividade);
    public TarefaKambamResponse ConsultarPorProjeto(int idProjeto, int idUsuario);
    public CronogramaAtividadeResponse ConsultarAtividadeCronogramaPorProjeto(int idProjeto);
}