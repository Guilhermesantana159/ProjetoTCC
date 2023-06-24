namespace Infraestrutura.Entity;
public class ComentarioTarefa
{
    public int? IdComentarioTarefa { get; set; }
    public string? Descricao { get; set; }
    public int IdTarefa { get; set; }
    public int IdUsuario { get; set; }
    public DateTime Data{ get; set; }

    #region Relacionamento
    public virtual Tarefa Tarefa {get; set; } = null!;
    public virtual Usuario Usuario {get; set; } = null!;
    #endregion
}