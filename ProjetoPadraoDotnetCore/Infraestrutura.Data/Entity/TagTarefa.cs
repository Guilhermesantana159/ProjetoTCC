namespace Infraestrutura.Entity;
public class TagTarefa
{
    public int IdTagTarefa { get; set; }
    public string? Descricao { get; set; }
    public int IdTarefa { get; set; }

    #region Relacionamento
    public virtual Tarefa Tarefa {get; set; } = null!;
    #endregion
}