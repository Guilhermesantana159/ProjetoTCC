using Infraestrutura.Enum;

namespace Infraestrutura.Entity;
public class ContatoChat
{
    public int? IdContatoChat { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public int IdUsuarioContato { get; set; }
    public DateTime DataCadastro{ get; set; }
    public StatusContato StatusContato { get; set; }

    #region Relacionamento
    public virtual Usuario UsuarioCadastro {get; set; }
    public virtual Usuario UsuarioContato {get; set; }
    #endregion
}