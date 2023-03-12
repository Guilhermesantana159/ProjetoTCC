namespace Aplication.Models.Request.Projeto;

public class ProjetoRequest
{
    public int IdProjeto { get; set; }
    public string? Titulo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string? Descricao { get; set; }
    public bool ListarParaParticipantes { get; set; }
}

public class AtividadeRequest
{
    public string? Atividade { get; set; }
    public DateTime DataInicial { get; set; }
    public DateTime DataFim { get; set; }
    public List<Tarefa>? ListTarefas{ get; set; }
}

public class TarefaRequest
{
    public int ResponsavelId { get; set; }
    public List<string>? Tarefa { get; set; }
}

public class Tarefa
{
    public string? Descricao { get; set; }
}