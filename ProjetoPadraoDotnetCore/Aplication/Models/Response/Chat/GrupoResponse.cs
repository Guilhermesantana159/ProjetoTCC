namespace Aplication.Models.Response.Chat;

public class GrupoResponse
{
    public int IdGrupo { get; set; }
    public string? Titulo { get; set; }
    public List<PessoaGrupo> Contatos { get; set; }
}

public class PessoaGrupo
{
    public int IdUsuario { get; set; }
    public string? Nome { get; set; }
    public string? Foto { get; set; }
}