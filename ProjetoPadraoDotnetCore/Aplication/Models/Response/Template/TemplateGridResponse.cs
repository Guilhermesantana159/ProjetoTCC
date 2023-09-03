namespace Aplication.Models.Response.Template;

public class TemplateGridResponse
{
    public int IdTemplate { get; set; }
    public string? TituloTemplate { get; set; }
    public string? Duracao { get; set; }
    public string? Categoria { get; set; }
    public string? Autor { get; set; }
    public bool? IsView { get; set; }
    public bool? IsEdit { get; set; }
    public string? FotoTemplate { get; set; }
}