namespace Infraestrutura.Entity;
public class rojetoAtividade
{
    public int IdProjeto { get; set; }
    public int IdAtividade { get; set; } 

    #region Relacionamento
    public virtual Projeto? Projeto { get; set; }
    public virtual Atividade? Atividade { get; set; }
    #endregion
}