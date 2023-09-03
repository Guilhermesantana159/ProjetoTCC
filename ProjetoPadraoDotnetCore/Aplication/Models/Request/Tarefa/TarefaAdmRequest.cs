using Infraestrutura.Enum;

namespace Aplication.Models.Request.Tarefa;

public class TarefaAdmRequest
{
    public int? IdTarefa { get; set; }
    public string? Descricao { get; set; }
    public List<int> LUsuarioIds { get;set;}
    public int? IdAtividade { get; set; }
    public string? DescricaoTarefa { get; set; }
    public List<string> LTagsTarefa { get;set;}
    public EPrioridadeTarefa Prioridade { get; set; } 
    public EStatusTarefa Status { get; set; }
}
