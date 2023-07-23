namespace Infraestrutura.Entity;
public class TagTarefaTemplate
{
    public int IdTagTarefaTemplate { get; set; }
    public string? Descricao { get; set; }
    public int? IdTarefaTemplate { get; set; }

    #region Relacionamento
    public virtual TarefaTemplate TarefaTemplate {get; set; } = null!;
    #endregion
}