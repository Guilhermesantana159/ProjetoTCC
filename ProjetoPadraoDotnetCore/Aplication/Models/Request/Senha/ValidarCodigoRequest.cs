namespace Aplication.Models.Request.Senha;

public class ValidarCodigoRequest
{
    public int? IdUsuario { get; set; }
    public string? Codigo { get; set; }
}