namespace Infraestrutura.Reports.Usuario.Objeto;

public class UsuarioGridReportObj
{
    public int IdUsuario { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public bool PerfilAdministrador { get; set; }
    public DateTime? DataNascimento { get; set; }
}