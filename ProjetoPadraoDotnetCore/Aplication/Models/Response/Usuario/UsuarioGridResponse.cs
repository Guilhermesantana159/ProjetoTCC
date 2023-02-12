namespace Aplication.Models.Response.Usuario;

public class UsuarioGridResponse
{
    public int IdUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string? Telefone { get; set; }
    public string Senha { get; set; }
    public string? DataNascimento { get; set; }
    public string Perfil { get; set; }
    public string ImagemUsuario { get; set; }
    public int Dedicacao { get; set; }
}