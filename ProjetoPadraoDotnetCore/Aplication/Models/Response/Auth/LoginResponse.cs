namespace Aplication.Models.Response.Auth;

public class LoginResponse
{
    public bool Autenticado { get; set; }
    public int IdUsuario { get; set; }
    public string Nome { get; set; }
    public object SessionKey { get; set; }
    public string Foto { get; set; }
    public bool Perfil { get; set; }
}