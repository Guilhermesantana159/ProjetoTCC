﻿using Infraestrutura.Enum;

namespace Infraestrutura.Entity;
public class Projeto
{
    public int IdProjeto { get; set; }
    public string? Titulo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string? Descricao { get; set; }
    public string? Foto { get; set; }
    public bool ListarParaParticipantes { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public DateTime DataCadastro { get; set; }
    public EStatusProjeto Status { get; set; }
    public bool EmailProjetoAtrasado { get; set; }
    public bool PortalProjetoAtrasado { get; set; }
    public bool EmailTarefaAtrasada { get; set; }
    public bool PortalTarefaAtrasada { get; set; }
    public bool AlteracaoStatusProjetoNotificar { get; set; }
    public bool AlteracaoTarefasProjetoNotificar { get; set; }

    #region Relacionamento
    public IEnumerable<Atividade> Atividades { get; set; } = null!;
    public virtual Usuario? Usuario { get; set; }
    #endregion
}