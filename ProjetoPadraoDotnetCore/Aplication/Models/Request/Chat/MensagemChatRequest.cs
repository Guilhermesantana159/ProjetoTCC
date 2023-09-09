namespace Aplication.Models.Request.Chat;

public class MensagemChatRequest 
{
    public int IdUsuarioMandante { get; set; }
    public int IdUsuarioRecebe { get; set; }
    public int? IdContatoRecebe { get; set; }
    public string? Message { get; set; }
    public string? ReplayName { get; set; }
    public string? ReplayMessage { get; set; }
}