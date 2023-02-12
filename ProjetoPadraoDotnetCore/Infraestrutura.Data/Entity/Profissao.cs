namespace Infraestrutura.Entity;

public class Profissao
{
    public int IdProfissao { get; set; }
    public string Descricao { get; set; } = null!;
    
    #region Relacionamento
    public IEnumerable<Usuario> LUsuario { get; set; } = null!;
    #endregion
}