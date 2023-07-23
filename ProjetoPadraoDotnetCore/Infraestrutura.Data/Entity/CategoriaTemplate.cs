namespace Infraestrutura.Entity;
public class CategoriaTemplate
{
    public int? IdCategoriaTemplate { get; set; }
    public string? Descricao { get; set; }
    public int? IdUsuarioCadastro { get; set; }
    public DateTime DataCadastro{ get; set; }

    #region Relacionamento
    public virtual Usuario UsuarioCadastro {get; set; } = null!;
    #endregion
}