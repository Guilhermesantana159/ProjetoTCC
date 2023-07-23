using Infraestrutura.Enum;

namespace Infraestrutura.Entity;
public class Template
{
    public int IdTemplate { get; set; }
    public string? Titulo { get; set; }
    public string? Foto { get; set; }
    public string? Descricao { get; set; }
    public EEscala? Escala { get; set; }
    public int QuantidadeTotal { get; set; }
    public int? IdTemplateCategoria { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public DateTime DataCadastro{ get; set; }

    #region Relacionamento
    public virtual Usuario UsuarioCadastro {get; set; } = null!;
    public virtual CategoriaTemplate CategoriaTemplate {get; set; } = null!;
    public IEnumerable<AtividadeTemplate>  LAtividadesTemplate {get; set; } = null!;
    #endregion
}