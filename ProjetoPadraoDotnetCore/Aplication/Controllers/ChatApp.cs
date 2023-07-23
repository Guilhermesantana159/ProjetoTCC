using Aplication.Interfaces;
using Aplication.Models.Request.Chat;
using Aplication.Models.Response.Chat;
using AutoMapper;
using Domain.Interfaces;
using Infraestrutura.Entity;

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
        
        var lContatos = ChatService.GetContatoById(request.IdUsuarioContato ?? 0);

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
                    StatusContato = x.StatusContato
                }).ToList()
        };
    }
}