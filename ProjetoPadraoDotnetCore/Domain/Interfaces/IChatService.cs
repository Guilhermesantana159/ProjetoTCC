using Infraestrutura.Entity;

namespace Domain.Interfaces;

public interface IChatService
{
    public ContatoChat CadastrarContatoComRetorno(ContatoChat contato);
    public void CadastrarContato(ContatoChat contato);
    public ContatoChat? GetContatoById(int idContatoChat);
    public void DeletarContato(int id);
    public void EditarContato(ContatoChat contato);
    public IQueryable<ContatoChat> GetContatosPessoaWithinclude();
    public MensagemChat CadastrarMensagemComRetorno(MensagemChat entity);
    public IQueryable<MensagemChat> GetMensagens(int idUsuarioMandante,int idUsuarioRecebe);
    public void DeletarMensagem(int id);
    public IQueryable<MensagemChat> GetMensagensWithInclude();
    public IQueryable<MensagemChat> GetAllMensagens();
    public void UpdateRange(List<MensagemChat> lEntity);
    public void DeleteRange(List<MensagemChat> lEntity);
}