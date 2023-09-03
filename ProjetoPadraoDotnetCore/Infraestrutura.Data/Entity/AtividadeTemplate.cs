namespace Infraestrutura.Entity;
public class AtividadeTemplate
{
    public int? IdAtvidadeTemplate { get; set; }
    public int? IdTemplate { get; set; }
    public int? TempoPrevisto { get; set; }
    public string? Titulo { get; set; }
    public int? Posicao { get; set; }

    #region Relacionamento
    public virtual Template Template {get; set; } = null!;
    public virtual IEnumerable<TarefaTemplate> LTarefaTemplate { get; set; } = null!;

    #endregion
}