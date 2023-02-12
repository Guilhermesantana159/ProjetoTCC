using Infraestrutura.Enum;

namespace Aplication.Models.Request.Usuario;

public class UsuarioRequest
{
    public int? IdUsuario { get; set; }
    public string? Nome { get; set; } = null!;
    public string? Foto { get; set; } = null!;
    public string? Senha { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public bool PerfilAdministrador { get; set; }
    public int? Dedicacao { get; set; }
    public int? IdUsuarioCadastro { get; set; }
    public string? Cep { get; set; } = null!;
    public string? Estado { get; set; } = null!;
    public string? Cidade { get; set; } = null!;
    public string? Pais { get; set; } = null!;
    public string? Rua { get; set; } = null!;
    public string? Bairro { get; set; } = null!;
    public int? Numero { get; set; }
    public string? NomeMae { get; set; } = null!;
    public string? NomePai { get; set; } = null!;
    public string? Cpf { get; set; } = null!;
    public string? Observacao { get; set; } = null!;
    public string? Rg { get; set; } = null!;
    public string? Telefone { get; set; } = null!;
    public DateTime? DataNascimento { get; set; }
    public EGenero Genero { get; set; }
    public int? IdProfissao { get; set; }
    public List<SkillRequest> lSkills { get; set; }
}
