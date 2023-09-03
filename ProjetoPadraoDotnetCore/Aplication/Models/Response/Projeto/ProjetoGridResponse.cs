namespace Aplication.Models.Response.Projeto;

public class ProjetoGridResponse
{
    public int IdProjeto { get; set; }
    public string? Titulo { get; set; }
    public string? FotoProjeto { get; set; }
    public string? DataInicio { get; set; }
    public string? DataFim { get; set; }
    public string? Status { get; set; }
    public string? Porcentagem { get; set; }
    public bool? ActionDisabled { get; set; }
    public bool DisabledView { get; set; }
    public bool DisabledAdminister { get; set; }
    public string? Administrador { get; set; }
}