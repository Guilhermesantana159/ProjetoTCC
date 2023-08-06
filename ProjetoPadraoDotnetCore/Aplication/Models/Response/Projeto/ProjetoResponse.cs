using Infraestrutura.Enum;

namespace Aplication.Models.Response.Projeto;

public class ProjetoResponse
{
    public int? IdProjeto { get; set; }
    public string? Titulo { get; set; }
    public EStatusProjeto Status { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string? Descricao { get; set; }
    public string? Foto { get; set; }
    public bool EmailProjetoAtrasado { get; set; } = true;
    public bool PortalProjetoAtrasado { get; set; } = true;
    public bool EmailTarefaAtrasada { get; set; } = true;
    public bool PortalTarefaAtrasada { get; set; } = true;
    public bool AlteracaoStatusProjetoNotificar { get; set; } = true;
    public bool AlteracaoTarefasProjetoNotificar { get; set; } = true;
    public bool ListarAtvProjeto { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public string? DataCadastro { get; set; }
    public List<AtvidadeResponse>? ListAtividade { get; set; }
    public List<TarefaResponse>? ListTarefa { get; set; }

}

public class AtvidadeResponse
{
    public int? IdAtividade { get; set; }
    public string? Atividade { get; set; }
    public string? DataInicial { get; set; }
    public string? DataFim { get; set; }
    public EStatusAtividade StatusAtividade { get; set; }
    public List<TarefaAtividadeResponse>? ListTarefas { get; set; }
}

public class TarefaResponse
{
    public string? Responsavel { get; set; }
    public int? IdResponsavel { get; set; }
    public List<TarefaAtividadeResponsavelResponse>? ListTarefas { get; set; }
}

public class TarefaAtividadeResponse
{
    public int? IdTarefa { get; set; }
    public string? Descricao { get; set; }
    public string? DescricaoTarefa { get; set; }
    public string? Prioridade { get; set; }
    public List<string?> LTagsTarefa { get; set; }
}

public class TarefaAtividadeResponsavelResponse
{
    public string? Tarefa { get; set; }
    public string? Atividade { get; set; }
}
