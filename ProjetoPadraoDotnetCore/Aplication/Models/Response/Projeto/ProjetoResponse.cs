namespace Aplication.Models.Response.Projeto;

public class ProjetoResponse
{
    public int IdProjeto { get; set; }
    public string? Titulo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string? Descricao { get; set; }
    public string? Foto { get; set; }
    public bool ListarAtvProjeto { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public DateTime DataCadastro { get; set; }
    public List<AtvidadeResponse>? ListAtividade { get; set; }
    public List<TarefaResponse>? ListTarefa { get; set; }

}

public class AtvidadeResponse
{
    public int IdAtividade { get; set; }
    public string? DataInicial { get; set; }
    public string? DataFim { get; set; }
    public List<TarefaAtividadeResponse>? ListTarefas { get; set; }
}

public class TarefaResponse
{
    public string? Responsavel { get; set; }
    public int IdResponsavel { get; set; }
    public List<TarefaAtividadeResponse>? ListTarefas { get; set; }
}

public class TarefaAtividadeResponse
{
    public string? Descricao { get; set; }
}