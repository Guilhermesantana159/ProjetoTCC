namespace Aplication.Models.Request.Profissao;

public class ProfissaoEditarRequest
{
    public int IdProfissao { get; set; }
    public string Descricao { get; set; } = null!;
}