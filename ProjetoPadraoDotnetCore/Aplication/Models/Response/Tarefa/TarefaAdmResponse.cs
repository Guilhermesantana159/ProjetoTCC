namespace Aplication.Models.Response.Tarefa;

public class TarefaAdmResponse
{ 
    public Indicadores? Indicadores { get; set; }
    public List<AtividadeAdmResponse>? ListAtividade { get; set; }
    public List<TarefaAdmListResponse>? ListTarefas { get; set; }
    public bool IsView { get; set; }
}

public class TarefaAdmListResponse
{
    public int IdTarefa { get; set; }
    public int IdAtividade { get; set; }
    public string? NomeAtividade { get; set; }
    public string? DataInicio { get; set; }
    public string? DataFim { get; set; }
    public int? Porcentagem { get; set; }
    public string? NomeTarefa { get; set; }
    public List<ResponsavelTarefa>? LResponsavelTarefa { get; set; }
    public string? Prioridade { get; set; }
    public int PrioridadeEnum { get; set; }
    public bool PermiteInicio { get; set; }
    public int StatusEnum { get; set; }
    public string? Status { get; set; }
    public List<string?> LTags { get; set; }
    public string? DescricaoTarefa { get; set; }
}

public class Indicadores
{
    public int TarefasFazer { get; set; }
    public int TarefasProgresso { get; set; }
    public int TarefasCompletas { get; set; }
    public int TarefasAtrasadas { get; set; }
}

public class ResponsavelTarefa
{
    public string? Foto { get; set; }
    public string? Nome { get; set; }
    public int? IdUsuario { get; set; }
}

public class AtividadeAdmResponse
{
    public List<string?> LTarefas { get; set; }
    public string? Nome { get; set; }
    public int IdAtividade { get; set; }
}