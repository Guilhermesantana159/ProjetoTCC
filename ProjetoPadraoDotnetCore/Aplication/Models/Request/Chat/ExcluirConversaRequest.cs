namespace Aplication.Models.Request.Chat;

public class ExcluirConversaRequest 
{
    public int IdUsuarioExclusao { get; set; }
    public List<int>? ListIdMensagemChat { get; set; } 
}