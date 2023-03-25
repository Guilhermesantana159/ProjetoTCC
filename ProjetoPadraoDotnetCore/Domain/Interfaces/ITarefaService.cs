using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface ITarefaService
{
    public Tarefa CadastrarComRetorno(Tarefa tarefa);
    public void DeleteRangeTarefas(List<Tarefa> tarefa);
}