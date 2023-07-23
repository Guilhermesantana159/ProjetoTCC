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
}