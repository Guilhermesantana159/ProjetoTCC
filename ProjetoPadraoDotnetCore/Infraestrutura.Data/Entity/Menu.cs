namespace Infraestrutura.Entity;

public class Menu
{
    public int IdMenu { get; set; }
    public string Nome { get; set; }
    public string Link { get; set; }
    public int IdSubModulo { get; set; }
    public bool OnlyAdmin { get; set; }

    #region Relacionamento
    public virtual SubModulo? SubModulo { get; set; }
    #endregion
}