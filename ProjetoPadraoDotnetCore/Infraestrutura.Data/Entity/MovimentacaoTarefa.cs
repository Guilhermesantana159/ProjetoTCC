using Infraestrutura.Enum;

namespace Infraestrutura.Entity;

public class MovimentacaoTarefa
{
    public int? IdMovimentacaoTarefa { get; set; }
    public int IdTarefa { get; set; }
    public int IdUsuarioMovimentacao { get; set; }
    public DateTime DataCadastro{ get; set; }
    public EStatusTarefa To{ get; set; }
    public EStatusTarefa From{ get; set; }
    public long TempoUtilizadoUltimaColuna { get; set; }
    
    #region Relacionamento
    public virtual Tarefa Tarefa {get; set; } = null!;
    public virtual Usuario Usuario {get; set; } = null!;
    #endregion
}