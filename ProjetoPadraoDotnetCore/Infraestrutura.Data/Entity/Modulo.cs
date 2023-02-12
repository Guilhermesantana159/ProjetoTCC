namespace Infraestrutura.Entity;

public class Modulo
{
    public int IdModulo { get; set; }
    public string Nome { get; set; }
    public string Icone { get; set; }
    public string DescricaoLabel { get; set; }
    public string DescricaoModulo { get; set; }

    #region Relacionamentos
    public IEnumerable<Menu> lMenus { get; set; }
    
    #endregion
}