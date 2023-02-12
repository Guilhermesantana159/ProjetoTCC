using Infraestrutura.Enum;

namespace Aplication.Models.Response.Base;

public class Retorno
{
    public bool Sucesso { get; set; }
    public string? Mensagem { get; set; }
    public object Data { get; set; }
    public StatusRetorno Status { get; set; }
}
