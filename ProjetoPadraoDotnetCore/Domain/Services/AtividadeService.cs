using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Atividade;

namespace Domain.Services;

public class AtividadeService : IAtividadeService
{
    protected readonly IAtividadeReadRepository ReadRepository;
    protected readonly IAtividadeWriteRepository WriteRepository;
    protected readonly ITarefaService TarefaService;

    public AtividadeService(IAtividadeReadRepository readRepository, IAtividadeWriteRepository writeRepository, ITarefaService tarefaService)
    {
        ReadRepository = readRepository;
        WriteRepository = writeRepository;
        TarefaService = tarefaService;
    }

    public Atividade CadastrarComRetorno(Atividade atividade)
    {
        return WriteRepository.AddWithReturn(atividade);
    }
    
    public Atividade EditarComRetorno(Atividade atividade)
    {
        return WriteRepository.UpdateWithReturn(atividade);
    }

    public Atividade? GetByIdWithInclude(int idAtividade)
    {
        return ReadRepository.GetByIdWithInclude(idAtividade);
    }
    
    public Atividade? GetById(int idAtividade)
    {
        return ReadRepository.GetById(idAtividade);
    }

    public List<Atividade> GetByIdProjeto(int idProjeto)
    {
        return ReadRepository.GetByIdProjeto(idProjeto);
    }

    public void DeleteById(Atividade atividade)
    {
        WriteRepository.DeleteById(atividade.IdAtividade);
    }

    public void DeleteRangeAtividades(List<Atividade> list)
    {
        foreach (var item in list)
        {
            TarefaService.DeleteRangeTarefas(item.Tarefas.ToList());
            WriteRepository.DeleteById(item.IdAtividade);
        }
    }
}