using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Repository.Interface.ContatoChat;
using Infraestrutura.Repository.Interface.MensagemChat;

namespace Domain.Services;

public class ChatService : IChatService
{
    protected IContatoChatReadRepository ContatoChatReadRepository;
    protected IContatoChatWriteRepository ContatoChatWriteRepository;
    protected IMensagemChatReadRepository MensagemChatReadRepository;
    protected IMensagemChatWriteRepository MensagemChatWriteRepository;

    public ChatService(IContatoChatReadRepository contatoChatReadRepository, IContatoChatWriteRepository contatoChatWriteRepository, IMensagemChatReadRepository mensagemChatReadRepository, IMensagemChatWriteRepository mensagemChatWriteRepository)
    {
        ContatoChatReadRepository = contatoChatReadRepository;
        ContatoChatWriteRepository = contatoChatWriteRepository;
        MensagemChatReadRepository = mensagemChatReadRepository;
        MensagemChatWriteRepository = mensagemChatWriteRepository;
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
    
    public MensagemChat CadastrarMensagemComRetorno(MensagemChat entity)
    {
        return MensagemChatWriteRepository.AddWithReturn(entity);
    }
    
    public IQueryable<MensagemChat> GetMensagens(int idUsuarioMandante,int idUsuarioRecebe)
    {
        return MensagemChatReadRepository.GetAll()
            .Where(x => ( x.IdUsuarioRecebe == idUsuarioRecebe) 
            || (x.IdUsuarioMandante ==  idUsuarioRecebe && x.IdUsuarioRecebe == idUsuarioMandante) && x.IdUsuarioExclusao != idUsuarioMandante);
    }
    
    public IQueryable<MensagemChat> GetAllMensagens()
    {
        return MensagemChatReadRepository.GetAll();
    }

    public void DeletarMensagem(int id)
    {
        MensagemChatWriteRepository.DeleteById(id);
    }
    
    public IQueryable<MensagemChat> GetMensagensWithInclude()
    {
        return MensagemChatReadRepository.GetMensagensWithInclude();
    }
    
    public void UpdateRange(List<MensagemChat> lEntity)
    {
        MensagemChatWriteRepository.UpdateRange(lEntity);
    }
    
    public void DeleteRange(List<MensagemChat> lEntity)
    {
        MensagemChatWriteRepository.DeleteRange(lEntity);
    }
}