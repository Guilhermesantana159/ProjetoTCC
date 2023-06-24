namespace Aplication.Models.Response.Tarefa;

public class TarefaDetalhesResponse
{
    public string? TempoUtilizado { get; set; }
    public string? Titulo { get; set; }
    public string? CodTarefa { get; set; }
    public string? NomeProjeto { get; set; }
    public string? NomeAtividade { get; set; }
    public string? Prioridade { get; set; }
    public string? Status { get; set; }
    public string? DataFim { get; set; }
    public List<ResponsavelTarefa>? ResponsavelTarefa { get; set; }
    public string? DescricaoTarefa { get; set; }
    public List<string?> LTags { get; set; }
    public List<ComentarioTarefaResponse>? LComentarios { get; set; }
    public bool IsView { get; set; }
    public List<MovimentacoesResponse> LMovimentacoes { get; set; } = null!;
}

public class ComentarioTarefaResponse
{
    public string? Foto { get; set; }
    public string? NomeUsuario { get; set; }
    public string? Horario { get; set; }
    public string? Comentario { get; set; }
}

public class MovimentacoesResponse
{
    public string? Foto { get; set; }
    public string? NomeUsuario { get; set; }
    public string? DataMovimentacao { get; set; }
    public string? TempoColuna { get; set; }
    public string? De { get; set; }
    public string? Para { get; set; }
    public string? TarefaNome { get; set; }
    public string? DataMovimentacaoFormatDate { get; set; }
}