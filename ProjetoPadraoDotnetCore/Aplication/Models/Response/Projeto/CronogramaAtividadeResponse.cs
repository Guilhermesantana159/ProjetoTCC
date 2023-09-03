namespace Aplication.Models.Response.Projeto;

public class CronogramaAtividadeResponse
{
    public string? DescricaoProjeto { get; set; }
    public string? DataInicio { get; set; }
    public string? DataFim { get; set; }
    public List<AtividadeCronogramaResponse>? LAtividadeCronograma { get; set; }
}

public class AtividadeCronogramaResponse
{
    public int IdAtividade { get; set; }
    public string? NomeAtividade { get; set; }
    public string? DataInicio { get; set; }
    public string? DataFim { get; set; }
}