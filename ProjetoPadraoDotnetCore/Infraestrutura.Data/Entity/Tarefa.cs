namespace Infraestrutura.Entity;
public class Tarefa
{
    public int IdTarefa { get; set; }
    public int IdAtividade { get; set; }
    public string? Descricao { get; set; }

    #region Relacionamento
    public virtual Atividade AtividadeFk {get; set; } = null!;
    public IEnumerable<TarefaUsuario> TarefaUsuario { get; set; } = null!;

    #endregion
}