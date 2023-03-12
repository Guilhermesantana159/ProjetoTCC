namespace Infraestrutura.Entity;
public class ProjetoAtividade
{
    public int IdProjeto { get; set; }
    public int IdAtividade { get; set; } 

    #region Relacionamento
    public virtual IEnumerable<Projeto>? Projeto { get; set; }
    public virtual IEnumerable<Atividade>? Atividade { get; set; }
    #endregion
}