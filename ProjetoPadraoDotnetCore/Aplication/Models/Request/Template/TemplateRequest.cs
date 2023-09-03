using System.Diagnostics.CodeAnalysis;
using Infraestrutura.Enum;

namespace Aplication.Models.Request.Template;

public class TemplateRequest
{
    public int? IdTemplate { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public string? Foto { get; set; }
    public EEscala? Escala { get; set; }
    public int? QuantidadeTotal { get; set; }
    public int? IdTemplateCategoria { get; set; }
    public string? DescricaoCategoriaNova { get; set; }
    public int? IdUsuarioCadastro { get; set; }
    public List<AtividadeTemplateRequest>? LAtividade { get; set; }
}

public class AtividadeTemplateRequest
{
    public int? TempoPrevisto { get; set; }
    public int? IdTemplate { get; set; }
    public string? Titulo { get; set; }
    public List<TarefaTemplateRequest>? LTarefaTemplate { get; set; }
    public int? Posicao { get; set; }
}

public class TarefaTemplateRequest
{
    public string? Descricao { get; set; }
    public string? DescricaoTarefa { get; set; }
    public EPrioridadeTarefa? Prioridade { get; set; }
    public List<string>? LTagsTarefa { get; set; }
}