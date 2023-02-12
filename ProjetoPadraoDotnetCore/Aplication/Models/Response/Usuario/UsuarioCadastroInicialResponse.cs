using Aplication.Models.Response.Auth;
using Aplication.Utils.Obj;

namespace Aplication.Models.Response.Usuario;

public class UsuarioCadastroInicialResponse
{
    public LoginResponse DataUsuario { get; set; } = null!;
    public ValidationResult Validacao { get; set; } = null!;
}