using Infraestrutura.Enum;

namespace Infraestrutura.Entity;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string Nome { get; set; } = null!;
    public string? Foto { get; set; }
    public string Email { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string? Telefone { get; set; }
    public string Senha { get; set; } = null!;
    public bool PerfilAdministrador { get; set; }
    public int Dedicacao { get; set; }
    public string? Cep { get; set; } 
    public string? Estado { get; set; } 
    public string? Cidade { get; set; } 
    public string? Pais { get; set; } 
    public string? Rua { get; set; } 
    public string? Bairro { get; set; } 
    public int? Numero { get; set; }
    public string? NomeMae { get; set; } 
    public string? NomePai { get; set; } 
    public string? Observacao { get; set; } 
    public string? Rg { get; set; } 
    public DateTime? DataNascimento { get; set; }
    public EGenero Genero { get; set; }
    public int? IdUsuarioCadastro { get; set; }
    public int? CodigoRecuperarSenha { get; set; }
    public int? TentativasRecuperarSenha { get; set; }
    public DateTime? DataRecuperacaoSenha { get; set; }
    public int? IdProfissao { get; set; }

    #region Relacionamento
    public virtual Profissao Profissao { get; set; } = null!;
    public virtual IEnumerable<SkillUsuario> LSkillUsuarios { get; set; } = null!;
    public virtual IEnumerable<Notificacao> LNotificacaoUsuarios { get; set; } = null!;
    public virtual Usuario UsuarioFk {get; set; } = null!;
    #endregion
}