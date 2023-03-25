namespace Aplication.Models.Request.Projeto;

public class ProjetoRequest
{
    public int? IdProjeto { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public string? Titulo { get; set; }
    public string? Foto { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public string? Descricao { get; set; }
    public bool ListarParaParticipantes { get; set; }
    public List<AtividadeRequest>? Atividade { get; set; }
    public List<TarefaUsuarioRequest>? Tarefa { get; set; }

}

public class AtividadeRequest
{
    public string? Atividade { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFim { get; set; }
    public List<TarefaRequest>? ListTarefas{ get; set; }
}

public class TarefaUsuarioRequest
{
    public int ResponsavelId { get; set; }
    public List<TarefaAtividadeRequest>? Tarefa { get; set; }
}

public class TarefaRequest
{
    public string? Descricao { get; set; }
}

public class TarefaAtividadeRequest
{
    public string? Tarefa { get; set; }
    public string? Atividade { get; set; }
}