using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ComentarioTarefa;
using Infraestrutura.Repository.Interface.MovimentacaoTarefa;
using Infraestrutura.Repository.Interface.TagTarefa;
using Infraestrutura.Repository.Interface.Tarefa;

namespace Domain.Services;

public class TarefaService : ITarefaService
{
    protected readonly IComentarioTarefaReadRepository ComentarioTarefaReadRepository;
    protected readonly IComentarioTarefaWriteRepository ComentarioTarefaWriteRepository;
    protected readonly IMovimentacaoTarefaReadRepository MovimentacaoTarefaReadRepository;
    protected readonly IMovimentacaoTarefaWriteRepository MovimentacaoTarefaWriteRepository;
    protected readonly ITarefaReadRepository ReadRepository;
    protected readonly ITarefaWriteRepository WriteRepository;
    protected readonly ITagTarefaWriteRepository TagWriteRepository;
    public TarefaService(ITarefaReadRepository readRepository, ITarefaWriteRepository writeRepository, ITagTarefaWriteRepository tagWriteRepository, IComentarioTarefaReadRepository comentarioTarefaReadRepository, IComentarioTarefaWriteRepository comentarioTarefaWriteRepository, IMovimentacaoTarefaReadRepository movimentacaoTarefaReadRepository, IMovimentacaoTarefaWriteRepository movimentacaoTarefaWriteRepository)
    {
        ReadRepository = readRepository;
        WriteRepository = writeRepository;
        TagWriteRepository = tagWriteRepository;
        ComentarioTarefaReadRepository = comentarioTarefaReadRepository;
        ComentarioTarefaWriteRepository = comentarioTarefaWriteRepository;
        MovimentacaoTarefaReadRepository = movimentacaoTarefaReadRepository;
        MovimentacaoTarefaWriteRepository = movimentacaoTarefaWriteRepository;
    }

    public Tarefa CadastrarComRetorno(Tarefa tarefa)
    {
        return  WriteRepository.AddWithReturn(tarefa);
    }
    
    public Tarefa EditarComRetorno(Tarefa tarefa)
    {
        if (tarefa.TarefaUsuario != null)
            WriteRepository.AddTarefaUsuario(tarefa.TarefaUsuario.ToList());

        return  WriteRepository.UpdateWithReturn(tarefa);
    }
    
    public void DeletarTarefasUsuario(List<TarefaUsuario> tarefa)
    { 
        WriteRepository.DeleteRangeTarefasUsuario(tarefa);
    }

    public IQueryable<Tarefa> GetTarefasPorAtividades(List<int> atividadesId)
    {
        return ReadRepository
            .GetAllWithInclude()
            .Where(x => atividadesId.Contains(x.IdAtividade));    
    }

    public void Cadastrar(Tarefa tarefa)
    {
        WriteRepository.Add(tarefa);
    }

    public void Editar(Tarefa tarefa)
    {
        WriteRepository.Update(tarefa);
    }

    public void DeletarTagsAntigos(List<TagTarefa> tagsTarefa)
    {
        if(tagsTarefa.Any())
            TagWriteRepository.DeleteRange(tagsTarefa);
    }
    
    public void DeletarTarefaUsuarioAntigos(List<TarefaUsuario> tarefaUsuario)
    {
        if(tarefaUsuario.Any())
            WriteRepository.DeleteRangeTarefasUsuario(tarefaUsuario);
    }

    public void DeleteRangeTarefas(List<Tarefa> tarefa)
    {
        WriteRepository.DeleteRangeTarefas(tarefa);
    }
    
    public IQueryable<Tarefa> GetTarefaByUsuario(List<int> atividadesId)
    {
        return ReadRepository
            .GetAllWithInclude()
            .Where(x => atividadesId.Contains(x.IdAtividade));
    }

    public Tarefa? GetTarefaByIdWithInclude(int tarefaId)
    {
        return ReadRepository
            .GetAllWithInclude()
            .FirstOrDefault(x => x.IdTarefa == tarefaId);
    }

    public void DeletarTarefaWithIncludes(Tarefa tarefa)
    {
        WriteRepository.DeleteTarefaWithIncludes(tarefa);
    }

    public void IntegrarComentarioTarefa(ComentarioTarefa comentario)
    {
        if (!comentario.IdComentarioTarefa.HasValue)
            ComentarioTarefaWriteRepository.AddWithReturn(comentario);
        else
            ComentarioTarefaWriteRepository.UpdateWithReturn(comentario); }
    
    public void DeletarComentarioTarefa(int idComentario)
    {
        ComentarioTarefaWriteRepository.DeleteById(idComentario);
    }

    public void DeletarComentarioTarefa(List<ComentarioTarefa> lcomentario)
    {
        ComentarioTarefaWriteRepository.DeleteRange(lcomentario);
    }

    public void IntegrarMovimentacaoTarefa(MovimentacaoTarefa entity)
    {
        MovimentacaoTarefaWriteRepository.Add(entity);
    }

    /// <summary>
    /// Consulta movimentacao por usuario e tarefa
    /// ou so usuario ou so tarefa
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <param name="idTarefa"></param>
    /// <returns></returns>
    public IQueryable<MovimentacaoTarefa>? GetMovimentacaoTarefaPorUsuarioTarefa(int? idUsuario,int? idTarefa)
    {
        if (idTarefa.HasValue && idUsuario.HasValue)
            return MovimentacaoTarefaReadRepository.GetAll().Where(x => x.IdUsuarioMovimentacao == idUsuario &&  x.IdTarefa == idTarefa);
        if (idUsuario.HasValue)
            return MovimentacaoTarefaReadRepository.GetAll().Where(x => x.IdUsuarioMovimentacao == idUsuario);
        if (idTarefa.HasValue)
            return MovimentacaoTarefaReadRepository.GetAll().Where(x => x.IdTarefa == idTarefa);

        return null;
    }

    public List<ComentarioTarefa> GetComentarioByIdTarefa(int idTarefa)
    {
        return ComentarioTarefaReadRepository.GetAll().Where(x => x.IdTarefa == idTarefa).ToList();
    }

    public Tarefa? GetTarefaById(int tarefaId)
    {
        return ReadRepository
            .GetById(tarefaId);
    }

    public IQueryable<TarefaUsuario> GetTarefaUsuario(List<int> atividadesId)
    {
        return ReadRepository
            .GetAllTarefaUsuarioWithInclude()
            .Where(x => atividadesId.Contains(x.Tarefa.IdAtividade));
    }
    
    public IQueryable<TarefaUsuario> GetTarefaUsuarioWithInclude()
    {
        return ReadRepository.GetAllTarefaUsuarioWithInclude();
    }
    
    public IQueryable<TarefaUsuario> GetTarefaUsuario()
    {
        return ReadRepository
            .GetAllTarefaUsuarioWithInclude();
    }
    
    public IQueryable<Tarefa> GetTarefaWithInclude()
    {
        return ReadRepository.GetAllWithInclude();
    }
}