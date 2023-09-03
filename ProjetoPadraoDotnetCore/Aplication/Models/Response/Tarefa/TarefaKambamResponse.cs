namespace Aplication.Models.Response.Tarefa;

public class TarefaKambamResponse
{
    public List<TarefaKambam>? LColumnTarefasKambam {get; set; }
    public List<ResponsavelTarefa>? ResponsavelTarefa { get; set; }
}

public class TarefaKambam
{
    public List<TaskKambam>? LTaskKambam { get; set; }
    public string? NomeColuna { get; set; }
    public int? QtdColuna { get; set; }
}

public class TaskKambam
{
    public string? NomeAtividade { get; set; }
    public string? NomeTarefa { get; set; }
    public string? DescricaoTarefa { get; set; }
    public List<string>? LTags { get; set; }
    public string? DataInicioAtv { get; set; }
    public int? Comentario { get; set; }
    public int? Progresso { get; set; }
}
