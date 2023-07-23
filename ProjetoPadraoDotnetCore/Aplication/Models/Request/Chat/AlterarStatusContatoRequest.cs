using Infraestrutura.Enum;

namespace Aplication.Models.Request.Chat;

public class AlterarStatusContatoRequest
{
    public int IdContatoChat { get; set; }
    public StatusContato NewStatus { get; set; }
}