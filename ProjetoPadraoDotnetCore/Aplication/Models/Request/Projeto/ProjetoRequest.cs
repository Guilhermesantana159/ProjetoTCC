using Infraestrutura.Enum;

namespace Aplication.Models.Request.Projeto;

public class ProjetoRequest
{
    public int? IdProjeto { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public string? Titulo { get; set; }
    public string? Foto { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string? Descricao { get; set; }
    public bool EmailProjetoAtrasado { get; set; }
    public bool PortalProjetoAtrasado { get; set; }
    public bool EmailTarefaAtrasada { get; set; }
    public bool PortalTarefaAtrasada { get; set; }
    public bool AlteracaoStatusProjetoNotificar { get; set; }
    public bool AlteracaoTarefasProjetoNotificar { get; set; }

    public List<AtividadeRequest>? Atividade { get; set; }
    public List<TarefaUsuarioRequest>? Tarefa { get; set; }

    public class AtividadeRequest
    {
        public int? IdAtividade { get; set; }
        public string? Atividade { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFim { get; set; }
        public EStatusAtividade StatusAtividade { get; set; }
        public List<TarefaRequest>? ListTarefas { get; set; }
    }

    public class TarefaUsuarioRequest
    {
        public int ResponsavelId { get; set; }
        public List<TarefaAtividadeRequest>? Tarefa { get; set; }
    }

    public class TarefaRequest
    {
        public int? IdTarefa { get; set; }
        public string? Descricao { get; set; }
        public string? DescricaoTarefa { get; set; }
        public EPrioridadeTarefa Prioridade { get; set; }
        public List<string>? LTagsTarefa { get; set; }
    }

    public class TarefaAtividadeRequest
    {
        public int? IdTarefa { get; set; }
        public string? Tarefa { get; set; }
        public string? Atividade { get; set; }
    }
}