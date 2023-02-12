using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.TipoNotificacao;

namespace Domain.Services;

public class TipoNotificacaoService : ITipoNotificacaoService
{
    protected readonly ITipoNotificacaoReadRepository ReadRepository;
    protected readonly ITipoNotificacaoWriteRepository WriteRepository;

    public TipoNotificacaoService(ITipoNotificacaoReadRepository readRepository,ITipoNotificacaoWriteRepository writeRepository)
    {
        ReadRepository = readRepository;
        WriteRepository = writeRepository;
    }

    public TipoNotificacao GetById(int id)
    {
        return ReadRepository.GetById(id);
    }

    public List<TipoNotificacao> GetAllList()
    {
        return ReadRepository.GetAll().ToList();
    }
    
    public IQueryable<TipoNotificacao> GetAllQuery()
    {
        return ReadRepository.GetAll();
    }

    public void Cadastrar(TipoNotificacao tipoNotificacao)
    {
        WriteRepository.Add(tipoNotificacao);
    }
    
    public TipoNotificacao CadastrarComRetorno(TipoNotificacao tipoNotificacao)
    {
       return WriteRepository.AddWithReturn(tipoNotificacao);
    }

    public void Editar(TipoNotificacao tipoNotificacao)
    {
        WriteRepository.Update(tipoNotificacao);
    }

    public void DeleteById(int id)
    {
        WriteRepository.DeleteById(id);
    }
}