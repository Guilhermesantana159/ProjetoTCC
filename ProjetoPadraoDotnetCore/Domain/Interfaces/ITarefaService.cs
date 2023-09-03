using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface ITarefaService
{
    public Tarefa CadastrarComRetorno(Tarefa tarefa);
    public void DeleteRangeTarefas(List<Tarefa> tarefa);
    public IQueryable<Tarefa> GetTarefaByUsuario(List<int> atividadesId);
    public Tarefa? GetTarefaById(int tarefaId);
    public IQueryable<TarefaUsuario> GetTarefaUsuario(List<int> atividadesId);
    public IQueryable<Tarefa> GetTarefaWithInclude();
    public IQueryable<TarefaUsuario> GetTarefaUsuario();
    public Tarefa EditarComRetorno(Tarefa tarefa);
    public void DeletarTarefasUsuario(List<TarefaUsuario> tarefa);
    public IQueryable<Tarefa> GetTarefasPorAtividades(List<int> atividadesId);
    public void Cadastrar(Tarefa tarefa);
    public void Editar(Tarefa tarefa);
    public void DeletarTagsAntigos(List<TagTarefa> tagsTarefa);
    public void DeletarTarefaUsuarioAntigos(List<TarefaUsuario> tarefaUsuario);
    public Tarefa? GetTarefaByIdWithInclude(int tarefaId);
    public void DeletarTarefaWithIncludes(Tarefa tarefa);
    public void IntegrarComentarioTarefa(ComentarioTarefa comentario);
    public List<ComentarioTarefa> GetComentarioByIdTarefa(int idTarefa);
    public void DeletarComentarioTarefa(int idComentario);
    public void DeletarComentarioTarefa(List<ComentarioTarefa> lcomentario);
    public void IntegrarMovimentacaoTarefa(MovimentacaoTarefa entity);
    public IQueryable<MovimentacaoTarefa>? GetMovimentacaoTarefaPorUsuarioTarefa(int? idUsuario,int? tarefa);
    public IQueryable<TarefaUsuario> GetTarefaUsuarioWithInclude();
}