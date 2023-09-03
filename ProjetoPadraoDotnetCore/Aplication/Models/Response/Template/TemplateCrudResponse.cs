using Aplication.Models.Request.Template;
using Infraestrutura.Enum;

namespace Aplication.Models.Response.Template;

public class TarefaCrudResponse
{
    public int? IdTemplate { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public EEscala? Escala { get; set; }
    public int QuantidadeTotal { get; set; }
    public int? IdTemplateCategoria { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public int? Posicao { get; set; }
    public List<AtividadeTemplateRequest>? LAtividade { get; set; }
}

