namespace Aplication.Models.Response.Tarefa;

public class MovimentacaoResponse
{
    public string? DataFimProjeto { get; set; }
    public string? NomeProjeto { get; set; }
    public string? DataInicioProjeto { get; set; }
    public string? DataFimAtividade { get; set; }
    public string? NomeAtividade { get; set; }
    public string? DataInicioAtividade { get; set; }
    public List<MovimentacoesResponse>? LMovimentacao { get; set; } 
}