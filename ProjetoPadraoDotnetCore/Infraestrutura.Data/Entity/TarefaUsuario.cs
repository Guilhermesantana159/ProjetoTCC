namespace Infraestrutura.Entity;
public class TarefaUsuario
{
    public int? IdTarefa { get; set; }
    public int? IdUsuario { get; set; }

    #region Relacionamento
    public virtual Tarefa Tarefa {get; set; } = null!;
    public virtual Usuario Usuario { get; set; } = null!;

    #endregion
}