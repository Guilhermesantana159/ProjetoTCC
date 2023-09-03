namespace Domain.DTO.Tarefa;

public class TarefaUsuarioDto
{
    public string? Responsavel { get; set; }
    public int IdResponsavel { get; set; }
    public List<Infraestrutura.Entity.Tarefa>? ListTarefas { get; set; }
}