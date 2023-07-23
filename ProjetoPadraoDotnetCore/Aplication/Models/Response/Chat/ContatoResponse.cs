using Infraestrutura.Enum;

namespace Aplication.Models.Response.Chat;

public class ContatoResponse 
{
    public string? Nome { get; set; }
    public string? Foto { get; set; }
    public StatusContato StatusContato { get; set; }
    public int IdUsuarioContato { get; set; }
    public int? IdContatoChat { get; set; }
}