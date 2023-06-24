using Aplication.Models.Request.Tarefa;
using Aplication.Models.Response.Tarefa;
using Aplication.Utils.Objeto;

namespace Aplication.Interfaces;

public interface ITarefaApp
{
   public ValidationResult Integrar(TarefaAdmRequest request);
   public TarefaAdmResponse ConsultarTarefasPorIdProjeto(int idProjeto);
   public TarefaRegistroResponse ConsultarTarefasRegistro(int idProjeto,int idUsuario);
   public ValidationResult DeletarTarefaById(int idTarefa);
   public ValidationResult IntegrarComentarioTarefa(ComentarioTarefaRequest request);
   public ValidationResult DeletarComentarioById(int? idComentario);
   public TarefaDetalhesResponse ConsultarDetalhesTarefa(int idTarefa);
   public ValidationResult IntegrarMovimentacao(MovimentacaoTarefaRequest request);
   public MovimentacaoResponse ConsultarMovimentacao(int idAtividade);
}