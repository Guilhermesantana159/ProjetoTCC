using Aplication.Utils.Objeto;

namespace Aplication.Models.Response.Usuario;

public class EnderecoResponse : ValidationResult
{ 
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Rua { get; set; } = null!;
    public bool StatusApi { get; set; }
}