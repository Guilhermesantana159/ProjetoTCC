namespace Aplication.Models.Response.Template;

public class TemplateResponse
{ 
    public int? IdTemplate { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public string? Foto { get; set; }
    public string? EscalaTempo { get; set; }
    public int? Quantidade { get; set; }
    public string? DataCadastro { get; set; }
    public string? Categoria { get; set; }
    public int? IdUsuarioCadastro { get; set; }
    public List<AtividadeTemplateResponse>? LAtividade { get; set; }
}

public class AtividadeTemplateResponse
{
    public int? TempoPrevisto { get; set; }
    public string? Titulo { get; set; }
    public List<TarefaTemplateResponse> LTarefaTemplate { get; set; }
    public int? Posicao { get; set; }
}

public class TarefaTemplateResponse
{
    public string? Descricao { get; set; }
    public string? DescricaoTarefa { get; set; }
    public string? Prioridade { get; set; }
    public List<string?> LTagsTarefa { get; set; }
}