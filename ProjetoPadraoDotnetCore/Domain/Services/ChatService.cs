using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ContatoChat;

namespace Domain.Services;

public class ChatService : IChatService
{
    protected IContatoChatReadRepository ContatoChatReadRepository;
    protected IContatoChatWriteRepository ContatoChatWriteRepository;


    public ChatService(IContatoChatReadRepository contatoChatReadRepository, IContatoChatWriteRepository contatoChatWriteRepository)
    {
        ContatoChatReadRepository = contatoChatReadRepository;
        ContatoChatWriteRepository = contatoChatWriteRepository;
    }

    public ContatoChat CadastrarContatoComRetorno(ContatoChat atividade)
    {
        return ContatoChatWriteRepository.AddWithReturn(atividade);
    }
    
    public void CadastrarContato(ContatoChat atividade)
    {
        ContatoChatWriteRepository.Add(atividade);
    }
    
    public ContatoChat? GetContatoById(int idContatoChat)
    {
        return ContatoChatReadRepository.GetById(idContatoChat);
    }

    public void DeletarContato(int id)
    {
        ContatoChatWriteRepository.DeleteById(id);
    }

    public void EditarContato(ContatoChat contato)
    {
        ContatoChatWriteRepository.Update(contato);
    }
    

    public IQueryable<ContatoChat> GetContatosPessoaWithinclude()
    {
        return ContatoChatReadRepository.GetAllWithIncludeQuery();
    }
}