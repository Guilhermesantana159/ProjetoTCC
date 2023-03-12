namespace Infraestrutura.Entity;
public class Atividade
{
    public int IdAtividade { get; set; } 
    public string? Titulo { get; set; }
    public DateTime DataInicial { get; set; }
    public DateTime DataFim { get; set; }

    #region Relacionamento
    public virtual IEnumerable<AtividadeTarefa> LAtividadeTarefa { get; set; } = null!;
    public virtual IEnumerable<ProjetoAtividade> LProjetoAtividade { get; set; } = null!;
    #endregion
}