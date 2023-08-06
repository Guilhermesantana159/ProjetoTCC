using Aplication.Models.Response.Base;
using Aplication.Models.Response.Tarefa;

namespace Aplication.Models.Response.Projeto;

public class ProjetoDashboardResponse
{
    public List<TarefaDashboard>? LTarefas { get; set; }
    public ProjetoDataDashboard? Projeto { get; set; }
    public List<SelectBase>? LProjetos { get; set; }
    public FeedbackDataDashboard? Feedback { get; set; }
}

public class TarefaDashboard
{
    public string? Projeto { get; set; }
    public List<string?>? LTarefa { get; set; }
}

public class ProjetoDataDashboard
{
    public int IdProjeto { get; set; }
    public Indicadores? LTarefaIndicador { get; set; }
    public List<AtividadeDataDashboard>? LAtividade { get; set; }
}

public class AtividadeDataDashboard
{
    public string? Atividade { get; set; }
    public Indicadores? Indicador { get; set; }
    
    public List<AtividadeTarefaDataDashboard>? LAtividadeTarefas { get; set; }
}

public class FeedbackDataDashboard
{
    public int? TotalFeedback { get; set; }
    public int? MediaFeedback { get; set; }
    public int? Estrela1 { get; set; }
    public int? Estrela2 { get; set; }
    public int? Estrela3 { get; set; }
    public int? Estrela4 { get; set; }
    public int? Estrela5 { get; set; }
}

public class AtividadeTarefaDataDashboard
{
    public long? TempoTotalTarefasEspera { get; set; }
    public long? TempoTotalTarefasProgresso { get; set; }
    public long? TempoTotalTarefasRealizado { get; set; }
    public long? TempoTotalTarefasTotal { get; set; }

    public List<TarefaIndicadoresDataDashboard>? LTarefas { get; set; }
}

public class TarefaIndicadoresDataDashboard
{
    public string? Tarefa { get; set; }
    public long? TempoTarefaEspera { get; set; }
    public long? TempoTarefaProgresso { get; set; }
    public long? TempoTarefaRealizado { get; set; }
    public long? TempoTarefaTotal { get; set; }
}






