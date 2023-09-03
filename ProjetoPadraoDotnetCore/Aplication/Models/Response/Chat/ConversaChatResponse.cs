using Infraestrutura.Enum;

namespace Aplication.Models.Response.Chat;

public class ConversaChatResponse
{
    public List<Mensagem>? MensagemChat { get; set; }
}

public class Mensagem
{
    public int IdMensagemChat { get; set; }
    public string? Message { get; set; }
    public string? DataCadastro { get; set; }
    public string? ReplayName { get; set; }
    public string? ReplayMessage { get; set; }
    public string? Align { get; set; }
    public int StatusMessage { get; set; }
}