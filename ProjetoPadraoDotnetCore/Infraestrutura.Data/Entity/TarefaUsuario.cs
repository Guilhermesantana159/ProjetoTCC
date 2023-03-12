namespace Infraestrutura.Entity;
public class TarefaUsuario
{
    public int IdTarefa { get; set; }
    public int IdUsuario { get; set; }

    #region Relacionamento
    public virtual IEnumerable<Usuario>? Usuario {get; set; }
    public virtual IEnumerable<Tarefa>? Tarefa {get; set; }
    #endregion
}