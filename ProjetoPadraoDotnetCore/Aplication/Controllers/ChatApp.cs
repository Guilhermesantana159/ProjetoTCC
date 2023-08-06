using Aplication.Interfaces;
using Aplication.Models.Request.Chat;
using Aplication.Models.Response.Chat;
using Aplication.Utils.Helpers;
using AutoMapper;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;

namespace Aplication.Controllers;

public class ChatApp : IChatApp
{
    protected readonly IMapper Mapper;
    private readonly IConfiguration _configuration;
    protected readonly IChatService ChatService;
    protected readonly IUsuarioService UsuarioService;

    public ChatApp(IConfiguration configuration,IMapper mapper, IChatService chatService, IUsuarioService usuarioService)
    {
        Mapper = mapper;
        ChatService = chatService;
        UsuarioService = usuarioService;
        _configuration = configuration;
    }

    public ContatoResponse Cadastrar(ContatoRequest request)
    {
        var usuarioContato = UsuarioService.GetById(request.IdUsuarioContato ?? 0);
        
        if (UsuarioService.GetById(request.IdUsuarioCadastro ?? 0) == null)
            throw new Exception("Usuário de cadastro não localizado!");
        
        if (usuarioContato == null)
            throw new Exception("Usuário contato não localizado!");

        var entity = ChatService.CadastrarContatoComRetorno(Mapper.Map<ContatoRequest,ContatoChat>(request));

        return new ContatoResponse()
        {
            Nome = usuarioContato.Nome,
            Foto = usuarioContato.Foto,
            StatusContato = entity.StatusContato,
            IdUsuarioContato = usuarioContato.IdUsuario,
            IdContatoChat = entity .IdContatoChat
        };
    }
    
    public void AlterarStatusContato(AlterarStatusContatoRequest request)
    {
        var entity = ChatService.GetContatoById(request.IdContatoChat);
        if (entity == null)
            throw new Exception("Contato não localizado!");

        //Novo status
        entity.StatusContato = request.NewStatus;
        
        ChatService.EditarContato(entity);
    }

    public void DeletarContato(int id)
    {
        var entity = ChatService.GetContatoById(id);
        if (entity == null)
            throw new Exception("Contato não localizado!");

        ChatService.DeletarContato(id);
    }

    public ContatoListaResponse ConsultarContatoPorIdPessoa(int id)
    {
        return new ContatoListaResponse()
        {
            LContatos = ChatService.GetContatosPessoaWithinclude()
                .Where(x => x.IdUsuarioCadastro == id)
                .Select(x => new ContatoResponse()
                {
                    Nome = x.UsuarioContato.Nome,
                    Foto = x.UsuarioContato.Foto,
                    IdUsuarioContato = x.IdUsuarioContato,
                    IdContatoChat = x.IdContatoChat,
                    StatusContato = x.StatusContato,
                    Sobre = x.UsuarioContato.Observacao,
                    Email = x.UsuarioContato.Email,
                    Telefone = x.UsuarioContato.Telefone,
                    DataNascimento = x.UsuarioContato.DataNascimento.HasValue ? x.UsuarioContato.DataNascimento.Value.FormatDateBr() : null
                }).ToList()
        };
    }

    public MensagemChatResponse SalvarMensagem(MensagemChatRequest request)
    {
        var retorno = ChatService.CadastrarMensagemComRetorno(Mapper.Map<MensagemChatRequest, MensagemChat>(request));

        return new MensagemChatResponse()
        {
            IdMensagemChat = retorno.IdMensagemChat
        };
    }

    public ConversaChatResponse ConsultarMensagens(int idUsuarioMandante,int idUsuarioRecebe)
    {
        var retorno = ChatService.GetMensagens(idUsuarioMandante,idUsuarioRecebe);

        return new ConversaChatResponse()
        {
            MensagemChat = retorno.Select(x => new Mensagem()
            {
                IdMensagemChat = x.IdMensagemChat,
                Message = x.Message,
                DataCadastro = x.DataCadastro.FormatLongDateBr(),
                ReplayName = x.ReplayName,
                ReplayMessage = x.ReplayMessage,
                Align = x.IdUsuarioMandante == idUsuarioMandante ? "right" : "left",
                StatusMessage = x.StatusMessage == EStatusMessage.Normal ? 0 : 1
            }).ToList()
        };
    }

    public void DeletarMensagem(int id)
    {
        ChatService.DeletarMensagem(id);
    }

    public ContatoListaResponse ConsultarMensagensDireta(int id)
    {
        return new ContatoListaResponse()
        {
            LContatos = ChatService.GetMensagensWithInclude()
                .Where(x => x.IdUsuarioMandante == id || x.IdUsuarioRecebe == id)
                .Distinct()
                .Select(x => new ContatoResponse()
                {
                    Nome = x.IdUsuarioMandante == id ? x.UsuarioRecebe.Nome: x.UsuarioMandante.Nome ,
                    Foto = x.IdUsuarioMandante == id ? x.UsuarioRecebe.Foto : x.UsuarioMandante.Foto,
                    IdUsuarioContato = x.IdUsuarioMandante == id ? x.IdUsuarioRecebe : x.IdUsuarioMandante,
                    IdContatoChat = x.IdUsuarioMandante == id ? null : x.IdContatoRecebe,
                    StatusContato = x.IdUsuarioMandante == id ? x.ContatoRecebeChat.StatusContato : StatusContato.Disponivel,
                    Sobre = x.IdUsuarioMandante == id ? x.UsuarioRecebe.Observacao : x.UsuarioMandante.Observacao ,
                    Email = x.IdUsuarioMandante == id ? x.UsuarioRecebe.Email : x.UsuarioMandante.Email,
                    Telefone = x.IdUsuarioMandante == id ? x.UsuarioMandante.Telefone : x.UsuarioRecebe.Telefone,
                    DataNascimento = x.IdUsuarioMandante == id 
                        ? (x.UsuarioRecebe.DataNascimento.HasValue ? x.UsuarioRecebe.DataNascimento.Value.FormatDateBr() : null)
                        : (x.UsuarioMandante.DataNascimento.HasValue ? x.UsuarioMandante.DataNascimento.Value.FormatDateBr() : null) 
                }).ToList()
        };    }

    public void ExcluirConversa(ExcluirConversaRequest request)
    {
        var usuario = UsuarioService.GetById(request.IdUsuarioExclusao);

        if (usuario == null)
            throw new Exception("Usuário não encontrado!");
        
        var lMensagens = ChatService.GetAllMensagens()
            .Where(x => request.ListIdMensagemChat != null 
                        && request.ListIdMensagemChat.Contains(x.IdMensagemChat));

        var lMensagensUpdate = lMensagens.Where(x => !x.IdUsuarioExclusao.HasValue).ToList();
        var lMensagensDelete = lMensagens.Where(x => x.IdUsuarioExclusao.HasValue).ToList();

        if (lMensagensDelete.Any())
            ChatService.DeleteRange(lMensagensDelete);

        if (lMensagensUpdate.Any())
        {
            foreach (var mensagem in lMensagensUpdate)
            {
                mensagem.IdUsuarioExclusao = request.IdUsuarioExclusao;
            }
            
            ChatService.UpdateRange(lMensagensUpdate);
        }
    }
}