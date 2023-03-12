namespace Infraestrutura.Entity;
public class AtividadeTarefa
{
    public int IdAtividade { get; set; } 
    public int IdTarefa { get; set; }

    #region Relacionamento
    public virtual  IEnumerable<Atividade>? Atividade { get; set; }
    public virtual IEnumerable<Tarefa>? Tarefa { get; set; }
    #endregion

}