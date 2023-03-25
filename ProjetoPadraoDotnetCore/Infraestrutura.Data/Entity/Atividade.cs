namespace Infraestrutura.Entity;
public class Atividade
{
    public int IdAtividade { get; set; }
    public string? Titulo { get; set; }
    public DateTime DataInicial { get; set; }
    public DateTime DataFim { get; set; }
    public int IdProjeto { get; set; }

    #region Relacionamento
    public virtual Projeto ProjetoFk { get; set; } = null!;
    public IEnumerable<Tarefa> Tarefas { get; set; } = null!;
    #endregion
}