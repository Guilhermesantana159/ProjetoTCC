using Infraestrutura.Enum;

namespace Infraestrutura.Entity;

public class Notificacao
{
    public int IdNotificacao { get; set; }
    public int IdUsuario { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataVisualização { get; set; }
    public string Corpo { get; set; } = null!;
    public string Titulo { get; set; } = null!;
    public EMensagemNotificacao ClassficacaoMensagem { get; set; }
    public ESimNao Lido { get; set; }

    #region Relacionamento
    public virtual Usuario Usuario { get; set; } = null!;
    #endregion
}