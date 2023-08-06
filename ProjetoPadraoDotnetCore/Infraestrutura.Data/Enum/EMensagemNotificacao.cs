using System.ComponentModel;

namespace Infraestrutura.Enum;

public enum EMensagemNotificacao
{
    [Description("MensagemBemVindo")]
    MensagemBemVindo = 0,
    [Description("ParticipacaoProjeto")]
    ParticipacaoProjeto = 1,
    [Description("ProjetoAtrasado")]
    ProjetoAtrasado = 2,
    [Description("AtividadeAtrasada")]
    AtividadeAtrasada = 3,
    [Description("MovimentacaoTarefa")]
    MovimentacaoTarefa = 4,
    [Description("ProjetoAlteracaoStatus")]
    ProjetoAlteracaoStatus = 5,
    [Description("ProjetoExcluido")]
    ProjetoExcluido = 6,
    [Description("TarefaExcluida")]
    TarefaExcluida = 7
}