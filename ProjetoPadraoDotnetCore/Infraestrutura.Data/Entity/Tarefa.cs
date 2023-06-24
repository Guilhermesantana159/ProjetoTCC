using Infraestrutura.Enum;

namespace Infraestrutura.Entity;
public class Tarefa
{
    public int IdTarefa { get; set; }
    public int IdAtividade { get; set; }
    public string? Descricao { get; set; }
    public EPrioridadeTarefa Prioridade { get; set; }
    public EStatusTarefa Status { get; set; }
    public string? DescricaoTarefa { get; set; }
    
    #region Relacionamento
    public virtual Atividade AtividadeFk {get; set; } = null!;
    public IEnumerable<TarefaUsuario> TarefaUsuario { get; set; } = null!;
    public IEnumerable<TagTarefa> TagTarefa { get; set; } = null!;
    public IEnumerable<ComentarioTarefa> ComentarioTarefa { get; set; } = null!;
    public IEnumerable<MovimentacaoTarefa> MovimentacaoTarefa { get; set; } = null!;

    #endregion
}