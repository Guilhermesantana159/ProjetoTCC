using Infraestrutura.Enum;

namespace Aplication.Models.Request.Usuario;

public class UsuarioRegistroInicialRequest
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Senha { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public EGenero Genero { get; set; }
}