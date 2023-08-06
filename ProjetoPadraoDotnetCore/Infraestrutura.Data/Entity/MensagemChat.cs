using Infraestrutura.Enum;

namespace Infraestrutura.Entity;
public class MensagemChat
{
  public int IdMensagemChat { get; set; }
  public int IdUsuarioMandante { get; set; }
  public int IdUsuarioRecebe { get; set; }
  public int IdContatoRecebe { get; set; }
  public string? Message { get; set; }
  public DateTime DataCadastro { get; set; }
  public string? ReplayName { get; set; }
  public string? ReplayMessage { get; set; }
  public EStatusMessage StatusMessage { get; set; }
  public int? IdUsuarioExclusao { get; set; }
  
  
  #region Relacionamento
  public virtual Usuario UsuarioRecebe { get; set; } = null!;
  public virtual Usuario? UsuarioExclusao { get; set; } = null!;
  public virtual Usuario UsuarioMandante { get; set; } = null!;
  public virtual ContatoChat ContatoRecebeChat { get; set; } = null!;
  #endregion
}

