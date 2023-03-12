﻿namespace Infraestrutura.Entity;
public class Projeto
{
    public int IdProjeto { get; set; }
    public string? Titulo { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string? Descricao { get; set; }
    public bool ListarParaParticipantes { get; set; }
    public int IdUsuarioCadastro { get; set; }
    public DateTime DataCadastro { get; set; }

    #region Relacionamento
    public virtual IEnumerable<ProjetoAtividade> LProjetoAtividade { get; set; } = null!;
    public virtual Usuario? Usuario { get; set; }
    #endregion
}