using Infraestrutura.Enum;

namespace Infraestrutura.Entity;
public class TarefaTemplate
{
    public int IdTarefaTemplate { get; set; }
    public string? Descricao { get; set; }
    public int IdAtividadeTemplate { get; set; }
    public string? DescricaoTarefa { get; set; }
    public EPrioridadeTarefa? Prioridade { get; set; }

    #region Relacionamento
    public virtual AtividadeTemplate AtividadeTemplate {get; set; } = null!;
    public IEnumerable<TagTarefaTemplate> TagTarefaTemplate { get; set; } = null!;
    #endregion
}