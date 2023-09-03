namespace Infraestrutura.Entity;

public class ContatoMensagem
{
    public int IdContatoMensagem { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Mensagem { get; set; }
    public DateTime? DataCadastro { get; set; }
}