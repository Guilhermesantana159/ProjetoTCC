namespace Aplication.Models.Request.Tarefa;

public class ComentarioTarefaRequest
{
    public int? IdComentarioTarefa { get; set; }
    public string? Descricao { get; set; }
    public int? IdTarefa { get; set; }
    public int? IdUsuario { get; set; }
}