namespace Infraestrutura.Entity;
public class ividadeTarefa
{
    public int IdAtividade { get; set; } 
    public int IdTarefa { get; set; }

    #region Relacionamento
    public virtual Atividade? Atividade { get; set; }
    public virtual Tarefa? Tarefa { get; set; }
    #endregion

}