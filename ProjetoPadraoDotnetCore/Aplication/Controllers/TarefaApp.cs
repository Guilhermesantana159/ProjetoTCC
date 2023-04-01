using Aplication.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Aplication.Controllers;

public class TarefaApp : ITarefaApp
{
    private readonly ITarefaService _service; 
    protected readonly IUsuarioService UsuarioService;
    protected readonly ITarefaService TarefaService;
    protected readonly INotificacaoService NotificaService;
    protected readonly IMapper Mapper;
    private readonly IConfiguration _configuration;

    public TarefaApp(ITarefaService service, INotificacaoService notificaService, IMapper mapper,IUsuarioService usuarioService, ITarefaService tarefaService, IConfiguration configuration)
    {
        _service = service;
        Mapper = mapper;
        UsuarioService = usuarioService;
        TarefaService = tarefaService;
        _configuration = configuration;
        NotificaService = notificaService;
    }

    
}