using Infraestrutura.Enum;

namespace Aplication.Models.Request.Tarefa;

public class MovimentacaoTarefaRequest
{
    public int IdTarefa { get; set; }
    public int IdUsuarioMovimentacao { get; set; }
    public EStatusTarefa To{ get; set; }
    public EStatusTarefa From{ get; set; }
}