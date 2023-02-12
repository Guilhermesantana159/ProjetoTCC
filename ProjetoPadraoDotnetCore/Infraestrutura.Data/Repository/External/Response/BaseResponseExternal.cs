using System.Net;

namespace Infraestrutura.Repository.External.Response;

public class BaseResponseExternal
{
    public HttpStatusCode? StatusCode { get; set; }
    public string? ObjetoJson { get; set; } = null!;
    public bool Sucesso { get; set; }
}