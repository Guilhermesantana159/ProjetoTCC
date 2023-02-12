using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.Notificacao;

namespace Domain.Services;

public class NotificacaoService : INotificacaoService
{
    protected readonly INotificacaoReadRepository ReadRepository;
    protected readonly INotificacaoWriteRepository WriteRepository;

    public NotificacaoService(INotificacaoReadRepository readRepository,INotificacaoWriteRepository writeRepository)
    {
        ReadRepository = readRepository;
        WriteRepository = writeRepository;
    }

    public Notificacao? GetById(int id)
    {
        return ReadRepository.GetById(id);
    }

    public List<Notificacao> GetAllList()
    {
        return ReadRepository.GetAll().ToList();
    }
    
    public IQueryable<Notificacao> GetAllQuery()
    {
        return ReadRepository.GetAll();
    }

    public void Cadastrar(Notificacao notificacao)
    {
        WriteRepository.Add(notificacao);
    }
    
    public Notificacao CadastrarComRetorno(Notificacao notificacao)
    {
       return WriteRepository.AddWithReturn(notificacao);
    }

    public void Editar(Notificacao? notificacao)
    {
        WriteRepository.Update(notificacao);
    }

    public void DeleteById(int id)
    {
        WriteRepository.DeleteById(id);
    }
    
    public void DeleteList(List<Notificacao> listNotificacao)
    {
        WriteRepository.DeleteRange(listNotificacao);
    }
}