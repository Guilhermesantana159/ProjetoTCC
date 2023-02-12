using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface ITipoNotificacaoService
{
    public TipoNotificacao GetById(int id);
    public List<TipoNotificacao> GetAllList();
    public IQueryable<TipoNotificacao> GetAllQuery();
    public void Cadastrar(TipoNotificacao tipoNotificacaoEntity);
    public TipoNotificacao CadastrarComRetorno(TipoNotificacao tipoNotificacaoEntity);
    public void Editar(TipoNotificacao tipoNotificacao);
    public void DeleteById(int id);
}