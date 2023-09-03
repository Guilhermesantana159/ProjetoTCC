namespace Infraestrutura.Entity;

public class Modulo
{
    public int IdModulo { get; set; }
    public string? Nome { get; set; }

    #region Relacionamento
    public IEnumerable<SubModulo> LSubModulo { get; set; }
    #endregion
}