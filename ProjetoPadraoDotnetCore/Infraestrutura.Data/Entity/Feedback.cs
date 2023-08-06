namespace Infraestrutura.Entity;
public class Feedback
{
  public int IdFeedback { get; set; }
  public int IdUsuarioCadastro { get; set; }
  public DateTime DataCadastro { get; set; }
  public int Rating { get; set; } 
  public string? Comentario { get; set; }

  #region Relacionamento
  public virtual Usuario UsuarioCadastro { get; set; } = null!;
  #endregion
}

