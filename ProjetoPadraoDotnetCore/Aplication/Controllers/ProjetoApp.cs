using System.Linq.Dynamic.Core;
using Aplication.Interfaces;
using Aplication.Models.Grid;
using Aplication.Models.Request.Projeto;
using Aplication.Models.Response.Projeto;
using Aplication.Utils.FilterDynamic;
using Aplication.Utils.Helpers;
using Aplication.Utils.Objeto;
using Aplication.Validators.Projeto;
using AutoMapper;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Projeto.Objeto;

namespace Aplication.Controllers;

public class ProjetoApp : IProjetoApp
{
    private readonly IProjetoService _service; 
    protected readonly IUsuarioService UsuarioService;
    protected readonly ITarefaService TarefaService;
    protected readonly IAtividadeService AtividadeService;
    protected readonly IProjetoValidator Validation;
    protected readonly INotificacaoService NotificaService;
    protected readonly IMapper Mapper;
    private readonly IConfiguration _configuration;

    public ProjetoApp(IProjetoService service, INotificacaoService notificaService, IMapper mapper, IProjetoValidator validation, IUsuarioService usuarioService, ITarefaService tarefaService, IAtividadeService atividadeService, IConfiguration configuration)
    {
        _service = service;
        Mapper = mapper;
        Validation = validation;
        UsuarioService = usuarioService;
        TarefaService = tarefaService;
        AtividadeService = atividadeService;
        _configuration = configuration;
        NotificaService = notificaService;
    }

    public ValidationResult Cadastrar(ProjetoRequest request)
    {
        var validation = Validation.ValidacaoCadastro(request);
        var usuarioCadastro = UsuarioService.GetById(request.IdUsuarioCadastro);
        var lUsuario = UsuarioService.GetAllQuery();

        Projeto projeto = null!;

        if (usuarioCadastro == null)
            validation.LErrors.Add("Falha ao salvar projeto!");
        else
        {
            projeto = Mapper.Map<ProjetoRequest,Projeto>(request);
            
            //Admin projeto
            projeto.IdUsuarioCadastro = usuarioCadastro.IdUsuario;
        }
        
        if(validation.IsValid())
        {
            var cadastro = _service.CadastrarComRetorno(projeto);

            //Atribuição tarefa por usuário
            if (request.Atividade != null)
            {
                foreach (var atv in request.Atividade)
                {
                    var atividadeEntity = Mapper.Map<AtividadeRequest, Atividade>(atv);

                    atividadeEntity.IdProjeto = projeto.IdProjeto;
                    
                    var atividade = AtividadeService.CadastrarComRetorno(atividadeEntity);

                    if (atv.ListTarefas != null)
                    {
                        foreach (var tarefa in atv.ListTarefas)
                        {
                            var listaUsuarioTarefa = new List<TarefaUsuario>();

                            //Adicionar admin projeto
                            if (usuarioCadastro != null)
                            {
                                listaUsuarioTarefa.Add(new TarefaUsuario()
                                {
                                    IdUsuario = usuarioCadastro.IdUsuario
                                });
                            }

                            if (request.Tarefa != null)
                            {
                                foreach (var userTarefa in request.Tarefa)
                                {
                                    if (userTarefa.ResponsavelId != projeto.IdUsuarioCadastro &&
                                        userTarefa.Tarefa != null
                                        && userTarefa.Tarefa.AsQueryable()
                                            .Any(x => x.Tarefa == tarefa.Descricao && x.Atividade == atv.Atividade))
                                    {
                                        var user = lUsuario.FirstOrDefault(x =>
                                            x.IdUsuario == userTarefa.ResponsavelId);

                                        if (user != null)
                                        {
                                            listaUsuarioTarefa.Add(new TarefaUsuario()
                                            {
                                                IdUsuario = user.IdUsuario
                                            });
                                        }
                                    }
                                }
                            }
                            
                            TarefaService.CadastrarComRetorno(new Tarefa()
                            {
                                Descricao = tarefa.Descricao,
                                IdAtividade = atividade.IdAtividade,
                                TarefaUsuario = listaUsuarioTarefa
                            });
                        }
                    }
                }    
            }

            if (request.Tarefa != null)
            {
                //Notificar participantes da sua participação do projeto
                foreach (var usuarioNotificacao in request.Tarefa)
                {
                    if (usuarioNotificacao.ResponsavelId != projeto.IdUsuarioCadastro)
                    {
                        var pushMensagem = new Notificacao()
                        {
                            IdUsuario = usuarioNotificacao.ResponsavelId,
                            DataCadastro = DateTime.Now,
                            Lido = ESimNao.Nao,
                            ClassficacaoMensagem = EMensagemNotificacao.ParticipacaoProjeto,
                            Corpo = $"Olá, você incluído como participante no projeto {cadastro.Titulo}. Este projeto é administrado e orientado pelo" +
                                    $" {cadastro.Usuario?.Nome},para acessar suas tarefas nesse projeto entre em \"Projetos > MinhasTarefas\" e acesse o respectivo projeto!",
                            Titulo = $"Participação no projeto {cadastro.Titulo}",
                            DataVisualização = null,
                        };
            
                        NotificaService.Cadastrar(pushMensagem);
                    }
                }
            }
        }
            

        return validation;
    }
    public BaseGridResponse ConsultarGridProjeto(ProjetoGridRequest request)
    {
        var itens = _service.GetAllQuery();
        var usuarioConsulta = UsuarioService.GetById(request.IdUsuario);

        if (usuarioConsulta != null)
        {
            if (!usuarioConsulta.PerfilAdministrador)
            {
                itens = itens.Where(x => x.IdUsuarioCadastro == usuarioConsulta.IdUsuarioCadastro);
            }
        }

        itens = string.IsNullOrEmpty(request.OrderFilters?.Campo)
            ? itens.OrderByDescending(x => x.IdProjeto)
            : itens.OrderBy($"{request.OrderFilters.Campo} {request.OrderFilters.Operador.ToString()}");

        itens = itens.AplicarFiltrosDinamicos(request.QueryFilters);
        
        return new BaseGridResponse()
        {
            Itens = itens.Skip(request.Page * request.Take).Take(request.Take)
                .Select(x => new ProjetoGridResponse()
                {
                    IdProjeto = x.IdProjeto,
                    Titulo = x.Titulo,
                    DataInicio = x.DataInicio.FormatDateBr(),
                    DataFim = x.DataFim.FormatDateBr(),
                    Status = x.Status.ToString(),
                    FotoProjeto = x.Foto == null 
                            ? _configuration.GetSection("ImageDefaultProjeto:Imagem").Value 
                            : x.Foto,
                    Porcentagem = x.DataInicio > DateTime.Now ? "0" : ((DateTime.Now - x.DataInicio) * 100/(x.DataFim - x.DataInicio).Days).ToString() 
                }).ToList(),
            
            TotalItens = itens.Count()
        };
    }
    public ValidationResult MudarStatusProjeto(ProjetoStatusRequest request)
    {
        var validation = new ValidationResult();
        var projeto = _service.GetByIdWithIncludes(request.IdProjeto);

        if (projeto == null)
            validation.LErrors.Add("Projeto não encontrado na base!");
        else
        {
            projeto.Status = request.Status;
            _service.Editar(projeto);
        }

        return validation;
    }
    
    public ValidationResult Deletar(ProjetoDeleteRequest request)
    {
        var validation = new ValidationResult();
        var projeto = _service.GetByIdWithIncludes(request.IdProjeto);

        if (projeto == null)
            validation.LErrors.Add("Projeto não encontrado na base!");
        else
        {
            //Delete Atividade / Tarefas
            AtividadeService.DeleteRangeAtividades(projeto.Atividades.ToList());
            //Delete Projeto
            _service.DeleteById(request.IdProjeto);
        }

        return validation;
    }
    
    public List<ProjetoGridReportObj> ConsultarRelatorioGridProjeto(ProjetoRelatorioGridRequest request)
    {
        var itens = _service.GetAllQuery();
        var usuarioConsulta = UsuarioService.GetById(request.IdUsuario);

        if (usuarioConsulta != null)
        {
            if (!usuarioConsulta.PerfilAdministrador)
            {
                itens = itens.Where(x => x.IdUsuarioCadastro == usuarioConsulta.IdUsuarioCadastro);
            }
        }

        itens = string.IsNullOrEmpty(request.OrderFilters?.Campo)
            ? itens.OrderByDescending(x => x.IdProjeto)
            : itens.OrderBy($"{request.OrderFilters.Campo} {request.OrderFilters.Operador.ToString()}");

        itens = itens.AplicarFiltrosDinamicos(request.QueryFilters);

        return itens
            .Select(x => new ProjetoGridReportObj
            {
                IdProjeto = x.IdProjeto,
                Titulo = x.Titulo,
                DataInicial = x.DataInicio.FormatDateBr(),
                DataFim = x.DataFim.FormatDateBr(),
                Status = x.Status.ToString(),
                Amdamento = "0%"
            }).ToList();

    }

}