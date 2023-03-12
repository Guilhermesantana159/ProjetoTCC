namespace Infraestrutura.Entity;
public class Tarefa
{
    public int IdTarefa { get; set; }
    public string? Descricao { get; set; }

    #region Relacionamento
    public virtual IEnumerable<AtividadeTarefa> LAtividadeTarefa { get; set; } = null!;
    public virtual IEnumerable<TarefaUsuario> LTarefaUsuario { get; set; } = null!;
    #endregion
}