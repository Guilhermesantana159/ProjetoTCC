using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface INotificacaoService
{
    public Notificacao? GetById(int id);
    public List<Notificacao> GetAllList();
    public IQueryable<Notificacao> GetAllQuery();
    public void Cadastrar(Notificacao notificacaoEntity);
    public Notificacao CadastrarComRetorno(Notificacao notificacaoEntity);
    public void Editar(Notificacao? notificacao);
    public void DeleteById(int id);
    public void DeleteList(List<Notificacao> listNotificacao);
}