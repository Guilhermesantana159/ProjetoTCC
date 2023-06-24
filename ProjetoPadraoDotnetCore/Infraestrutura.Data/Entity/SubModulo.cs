namespace Infraestrutura.Entity;

public class SubModulo
{
    public int IdSubModulo { get; set; }
    public string? Nome { get; set; }
    public string? Icone { get; set; }
    public int IdModulo { get; set; }

    #region Relacionamentos
    public IEnumerable<Menu>? LMenus { get; set; }
    public virtual Modulo? Modulo { get; set; }
    #endregion
}