using System.Globalization;
using System.Linq.Dynamic.Core;
using Aplication.Interfaces;
using Aplication.Models.Grid;
using Aplication.Models.Request.Projeto;
using Aplication.Models.Response.Base;
using Aplication.Models.Response.Projeto;
using Aplication.Models.Response.Tarefa;
using Aplication.Utils.FilterDynamic;
using Aplication.Utils.Helpers;
using Aplication.Utils.Objeto;
using Aplication.Validators.Projeto;
using AutoMapper;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Projeto.Objeto;
using Infraestrutura.Repository.Interface.Feedback;
using Infraestrutura.Repository.Interface.MovimentacaoTarefa;

namespace Aplication.Controllers;

public class ProjetoApp : IProjetoApp
{
    private readonly IProjetoService _service; 
    protected readonly IUsuarioService UsuarioService;
    protected readonly ITarefaService TarefaService;
    protected readonly ITarefaApp TarefaApp;
    protected readonly IAtividadeService AtividadeService;
    protected readonly IProjetoValidator Validation;
    protected readonly INotificacaoService NotificaService;
    protected readonly IMapper Mapper;
    protected readonly IFeedbackReadRepository FeedbackReadRepository;
    protected readonly IMovimentacaoTarefaReadRepository MovimentacaoTarefaReadRepository;
    private readonly IConfiguration _configuration;

    public ProjetoApp(IProjetoService service, INotificacaoService notificaService, IMapper mapper, IProjetoValidator validation, IUsuarioService usuarioService, ITarefaService tarefaService, IAtividadeService atividadeService, IConfiguration configuration, ITarefaApp tarefaApp, IFeedbackReadRepository feedbackReadRepository, IMovimentacaoTarefaReadRepository movimentacaoTarefaReadRepository)
    {
        _service = service;
        Mapper = mapper;
        Validation = validation;
        UsuarioService = usuarioService;
        TarefaService = tarefaService;
        AtividadeService = atividadeService;
        _configuration = configuration;
        TarefaApp = tarefaApp;
        this.FeedbackReadRepository = feedbackReadRepository;
        MovimentacaoTarefaReadRepository = movimentacaoTarefaReadRepository;
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
                    var atividadeEntity = Mapper.Map<ProjetoRequest.AtividadeRequest, Atividade>(atv);

                    atividadeEntity.IdProjeto = projeto.IdProjeto;

                    var atividade = AtividadeService.CadastrarComRetorno(atividadeEntity);

                    if (atv.ListTarefas != null)
                    {
                        foreach (var tarefa in atv.ListTarefas)
                        {
                            var listaUsuarioTarefa = new List<TarefaUsuario>();

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
                                Prioridade = tarefa.Prioridade,
                                DescricaoTarefa = tarefa.DescricaoTarefa,
                                TagTarefa = tarefa.LTagsTarefa?.Select(x => new TagTarefa()
                                {
                                    Descricao = x
                                }).ToList(), 
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
                            Corpo = $"Olá, você foi incluído como participante no projeto {cadastro.Titulo}. Este projeto é administrado e orientado por" +
                                    $" {usuarioCadastro?.Nome},para acessar suas tarefas nesse projeto entre em \"Tarefas > Registro\"!",
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

    public ValidationResult Editar(ProjetoRequest request)
    {
        var validation = Validation.ValidacaoCadastro(request);
        var lUsuario = UsuarioService.GetAllQuery();
        var projetoOld = _service.GetByIdWithIncludes(request.IdProjeto ?? 0);
        var lAtividadeId = new List<int>();

        Projeto projeto = null!;

        if (projetoOld == null)
            validation.LErrors.Add("Falha ao salvar projeto!");
        else
        {
            projeto = Mapper.Map<ProjetoRequest,Projeto>(request);

            //Dados remanescente
            projeto.IdUsuarioCadastro = projetoOld.IdUsuarioCadastro;
            projeto.DataCadastro = projetoOld.DataCadastro;
        }
        
        if(validation.IsValid())
        {
            //Remoção tarefas por usuário antigos
            if (projetoOld != null)
            {
                foreach (var item in projetoOld.Atividades)
                {
                    if (item.Tarefas.Any())
                    {
                        foreach (var tarefaUsuario in item.Tarefas)
                        {
                            TarefaService.DeletarTarefasUsuario(tarefaUsuario.TarefaUsuario.ToList());
                        }
                    }
                    
                    lAtividadeId.Add(item.IdAtividade);
                }
            }
            
            var lTarefas = TarefaService.GetTarefaWithInclude()
                .Where(x => lAtividadeId.Contains(x.IdAtividade)).ToList();

            foreach (var tarefa in lTarefas)
            {
                if (tarefa.TagTarefa.Any())
                {
                    TarefaService.DeletarTagsAntigos(tarefa.TagTarefa.ToList());
                }
            }
            
            var cadastro = _service.EditarComRetorno(projeto);

            //Atribuição tarefa por usuário
            if (request.Atividade != null)
            {
                foreach (var atv in request.Atividade)
                {
                    var atividadeEntity = Mapper.Map<ProjetoRequest.AtividadeRequest, Atividade>(atv);

                    atividadeEntity.IdProjeto = projeto.IdProjeto;

                    Atividade? atividade;
                    
                    if (atv.IdAtividade.HasValue)
                        atividade = AtividadeService.EditarComRetorno(atividadeEntity);
                    else
                        atividade = AtividadeService.CadastrarComRetorno(atividadeEntity);

                    if (atv.ListTarefas != null)
                    {
                        foreach (var tarefa in atv.ListTarefas)
                        {
                            var listaUsuarioTarefa = new List<TarefaUsuario>();

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
                                            if (tarefa.IdTarefa.HasValue)
                                            {
                                                listaUsuarioTarefa.Add(new TarefaUsuario()
                                                {
                                                    IdTarefa = tarefa.IdTarefa,
                                                    IdUsuario = user.IdUsuario
                                                });
                                            }
                                            else
                                            {
                                                listaUsuarioTarefa.Add(new TarefaUsuario()
                                                {
                                                    IdUsuario = user.IdUsuario
                                                });
                                            }
                                        }
                                    }
                                }
                            }

                            if (tarefa.IdTarefa.HasValue)
                            {
                                TarefaService.EditarComRetorno(new Tarefa()
                                {
                                    IdTarefa = tarefa.IdTarefa ?? 0,
                                    DescricaoTarefa = tarefa.DescricaoTarefa,
                                    TagTarefa = tarefa.LTagsTarefa?.Select(x => new TagTarefa()
                                    {
                                        Descricao = x
                                    }).ToList(), 
                                    Descricao = tarefa.Descricao,
                                    IdAtividade = atividade.IdAtividade,
                                    TarefaUsuario = listaUsuarioTarefa
                                });
                            }
                            else
                            {
                                TarefaService.CadastrarComRetorno(new Tarefa()
                                {
                                    Prioridade = tarefa.Prioridade,
                                    DescricaoTarefa = tarefa.DescricaoTarefa,
                                    TagTarefa = tarefa.LTagsTarefa?.Select(x => new TagTarefa()
                                    {
                                        Descricao = x
                                    }).ToList(), 
                                    Descricao = tarefa.Descricao,
                                    IdAtividade = atividade.IdAtividade,
                                    TarefaUsuario = listaUsuarioTarefa
                                });
                            }
                        }
                    }
                }
            }
        }
        
        return validation;    
    }

    public BaseGridResponse ConsultarGridProjeto(ProjetoGridRequest request)
    {
        var itens = _service.GetAllWithIncludeQuery();
        var usuarioConsulta = UsuarioService.GetById(request.IdUsuario);

        if (usuarioConsulta != null)
        {
            if (!usuarioConsulta.PerfilAdministrador)
            {
                if (!request.OnlyAdmin)
                {
                    itens = itens
                        .Where(x => x.IdUsuarioCadastro == usuarioConsulta.IdUsuario || x.Atividades
                            .Any(y => y.Tarefas
                                .Any(z => z.TarefaUsuario
                                    .Any(v => v.IdUsuario == request.IdUsuario))));
                }
                else
                    itens = itens.Where(x => x.IdUsuarioCadastro == usuarioConsulta.IdUsuario);
            }
        }
        
        if (request.OnlyAbertos)
            itens = itens.Where(x => x.Status == EStatusProjeto.Aberto || x.Status == EStatusProjeto.Atrasado);

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
                    Status = x.Status == EStatusProjeto.Cancelado || x.Status == EStatusProjeto.Concluido ? x.Status.ToString()
                        : x.DataFim < DateTime.Now ? EStatusProjeto.Atrasado.ToString() : x.Status.ToString(),
                    ActionDisabled = x.Status != EStatusProjeto.Aberto && x.Status != EStatusProjeto.Atrasado,
                    Administrador = x.Usuario != null ? x.Usuario.Nome : "",
                    FotoProjeto = x.Foto ?? _configuration.GetSection("ImageDefaultProjeto:Imagem").Value,
                    Porcentagem = (x.Status == EStatusProjeto.Cancelado || x.Status == EStatusProjeto.Concluido) ? "100"
                        : x.DataInicio > DateTime.Now 
                            ? "0" : 
                            Convert.ToInt64((DateTime.Now - x.DataInicio).TotalDays / (x.DataFim - x.DataInicio).TotalDays * 100) > 0 
                                ? Convert.ToInt64((DateTime.Now - x.DataInicio).TotalDays / (x.DataFim - x.DataInicio).TotalDays * 100).ToString(CultureInfo.InvariantCulture) 
                                : (Convert.ToInt64((DateTime.Now - x.DataInicio).TotalDays / (x.DataFim - x.DataInicio).TotalDays * 100) * -1).ToString(CultureInfo.InvariantCulture),
                    DisabledView = x.Status == EStatusProjeto.Aberto || x.Status == EStatusProjeto.Atrasado,
                    DisabledAdminister = x.Status != EStatusProjeto.Aberto && x.Status != EStatusProjeto.Atrasado
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
            var statusOld = projeto.Status;
            projeto.Status = request.Status;
            _service.Editar(projeto);
            
            var listUsuario = new List<Usuario>();
            var lTarefas = TarefaService
                .GetTarefasPorAtividades(projeto.Atividades.Select(x => x.IdAtividade)
                    .ToList()).ToList();

            foreach (var tarefa in lTarefas)
            {
                foreach (var tarefaUsuario in tarefa.TarefaUsuario.ToList())
                {
                    if (!listUsuario.Contains(tarefaUsuario.Usuario))
                    {
                        listUsuario.Add(tarefaUsuario.Usuario);
                    }
                }
            }

            if (projeto.AlteracaoStatusProjetoNotificar)
            {
                foreach (var usuario in listUsuario)
                {
                    var pushMensagem = new Notificacao()
                    {
                        IdUsuario = usuario.IdUsuario,
                        DataCadastro = DateTime.Now,
                        Lido = ESimNao.Nao,
                        ClassficacaoMensagem = EMensagemNotificacao.ProjetoAlteracaoStatus,
                        Corpo = $"O projeto {projeto.Titulo} teve seu status alterado de {statusOld.ToString()} para {projeto.Status.ToString()}!",
                        Titulo = $"O projeto {projeto.Titulo} teve seu status alterado!",
                        DataVisualização = null,
                    };
            
                    NotificaService.Cadastrar(pushMensagem);
                }    
            }
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
            //Notificação
            var listUsuario = new List<Usuario>();
            var lTarefas = TarefaService
                .GetTarefasPorAtividades(projeto.Atividades.Select(x => x.IdAtividade)
                    .ToList()).ToList();

            foreach (var tarefa in lTarefas)
            {
                foreach (var tarefaUsuario in tarefa.TarefaUsuario.ToList())
                {
                    if (!listUsuario.Contains(tarefaUsuario.Usuario))
                    {
                        listUsuario.Add(tarefaUsuario.Usuario);
                    }
                }
            }

            if (projeto.AlteracaoStatusProjetoNotificar)
            {
                foreach (var usuario in listUsuario)
                {
                    var pushMensagem = new Notificacao()
                    {
                        IdUsuario = usuario.IdUsuario,
                        DataCadastro = DateTime.Now,
                        Lido = ESimNao.Nao,
                        ClassficacaoMensagem = EMensagemNotificacao.ProjetoExcluido,
                        Corpo = $"O projeto {projeto.Titulo} que você participou foi excluído!",
                        Titulo = $"O projeto {projeto.Titulo} que você participou foi excluído!",
                        DataVisualização = null,
                    };
            
                    NotificaService.Cadastrar(pushMensagem);
                }
            }

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
    
    public ProjetoResponse GetById(int id)
    { 
        var projeto = _service.GetByIdWithIncludes(id);
        var lIdsAtividade = new List<int>();
        var retorno = Mapper.Map<ProjetoResponse>(projeto);

        if (projeto == null)
            throw new Exception($"Não foi possível encontrar o projeto com id {id}");

        //AdicionarAtividade
        foreach (var item in projeto.Atividades)
        {
            //Verificação se a atividade foi concluída 
            if (item.StatusAtividade != EStatusAtividade.Completo && item.Tarefas.All(x => x.Status == EStatusTarefa.Completo))
            {
                item.StatusAtividade = EStatusAtividade.Completo;
                AtividadeService.EditarComRetorno(item);
            } 
            else if ((DateTime.Now > item.DataFim && item.StatusAtividade != EStatusAtividade.Atrasado && item.Tarefas.All(x => x.Status != EStatusTarefa.Completo)) 
                     || (projeto.DataFim.Date < DateTime.Now.Date && projeto.Status != EStatusProjeto.Concluido || projeto.Status != EStatusProjeto.Cancelado))
            {
                item.StatusAtividade = EStatusAtividade.Atrasado;
                AtividadeService.EditarComRetorno(item);
            }
            else if (item.DataFim > DateTime.Now && item.StatusAtividade != EStatusAtividade.Progresso && item.Tarefas.All(x => x.Status != EStatusTarefa.Completo))
            {
                item.StatusAtividade = EStatusAtividade.Progresso;
                AtividadeService.EditarComRetorno(item);
            }
            
            var atividade = new AtvidadeResponse()
            {
                IdAtividade = item.IdAtividade,
                Atividade = item.Titulo,
                StatusAtividade = DateTime.Now > item.DataFim && item.StatusAtividade == EStatusAtividade.Progresso? EStatusAtividade.Atrasado :  item.StatusAtividade,
                DataFim = item.DataFim.FormatDateBr(),
                DataInicial = item.DataInicial.FormatDateBr(),
                ListTarefas = item.Tarefas.AsQueryable().Select(x => new TarefaAtividadeResponse
                {
                    Descricao = x.Descricao,
                    IdTarefa = x.IdTarefa,
                    LTagsTarefa = new List<string?>(),
                    DescricaoTarefa = x.DescricaoTarefa,
                    Prioridade = x.Prioridade.GetHashCode().ToString()
                }).ToList()
            };


            if (retorno.ListAtividade != null)
            {
                retorno.ListAtividade.Add(atividade);
                lIdsAtividade.Add(item.IdAtividade);
            }
        }
        
        //AdicionarAtividade
        var lTarefas = TarefaService.GetTarefaWithInclude()
            .Where(x => lIdsAtividade.Contains(x.IdAtividade));
        
        var lTarefasId = lTarefas.Select(x => x.IdTarefa);

        var lUsuarioTarefa = TarefaService
            .GetTarefaUsuario()
            .Where(x => lTarefasId.Contains(x.IdTarefa ?? 0))
            .ToList()
            .GroupBy(x => x.Usuario);

        if (retorno.ListAtividade != null)
        {
            foreach (var atv in retorno.ListAtividade)
            {
                if (atv.ListTarefas == null) continue;
                
                foreach (var tarefa in atv.ListTarefas)
                {
                    var lTags = lTarefas.FirstOrDefault(x => x.IdTarefa == tarefa.IdTarefa)?.TagTarefa
                        .Select(y => y.Descricao).ToList();

                    if (lTags != null) tarefa.LTagsTarefa.AddRange(lTags);
                }
            }
        }
        
        foreach (var userTarefa in lUsuarioTarefa)
        {
            if (userTarefa.Key?.TarefaUsuario != null)
            {
                var tarefaResponse = new TarefaResponse()
                {
                    IdResponsavel = userTarefa.Key?.IdUsuario,
                    Responsavel = userTarefa.Key?.Nome,
                    ListTarefas = userTarefa.Key?.TarefaUsuario
                        .Select(x => x.Tarefa)
                        .Select(y => new TarefaAtividadeResponsavelResponse
                        {
                            Atividade = y?.AtividadeFk.Titulo,
                            Tarefa = y?.Descricao,
                            
                        }).ToList()
                };
                
                if (retorno.ListTarefa != null)
                    retorno.ListTarefa.Add(tarefaResponse);
            }
        }

        return retorno;
    }

    public void DeletarAtividade(int idAtividade)
    {
        var atividade = AtividadeService.GetByIdWithInclude(idAtividade);

        if (atividade == null)
            throw new Exception("Atividade não encontrada!");
            
        if (atividade.Tarefas != null)
        {
            foreach (var item in atividade.Tarefas)
            {
                TarefaApp.DeletarTarefaById(item.IdTarefa);
            }
            
            AtividadeService.DeleteById(atividade);
        }
    }

    public TarefaKambamResponse ConsultarPorProjeto(int idProjeto, int idUsuario)
    {
        var retorno = new TarefaKambamResponse()
        {
            LColumnTarefasKambam = new List<TarefaKambam>(),
            ResponsavelTarefa = new List<ResponsavelTarefa>()
        };
        
        var projeto = _service.GetById(idProjeto);
        
        if (projeto == null)
            throw new Exception("Não foi encontrado nenhum registro de projeto!");
        
        var lIdsprojeto = AtividadeService.GetByIdProjeto(idProjeto).Select(x => x.IdProjeto).ToList();
        
        if (lIdsprojeto == null || !lIdsprojeto.Any())
            throw new Exception("Não foi encontrado nenhum registro de atividade para este projeto!");
        
        var lTarefas = TarefaService.GetTarefasPorAtividades(lIdsprojeto);

        if (projeto.IdUsuarioCadastro != idUsuario)
            lTarefas = lTarefas.Where(x => x.TarefaUsuario.Any(y => y.IdUsuario == idUsuario));

        foreach (var item in lTarefas)
        {
            foreach (var tarefaUsuario in item.TarefaUsuario)
            {
                if (retorno.ResponsavelTarefa.All(x => x.IdUsuario != tarefaUsuario.IdUsuario))
                {
                    retorno.ResponsavelTarefa.Add(new ResponsavelTarefa()
                    {
                        IdUsuario = tarefaUsuario.IdUsuario,
                        Foto = tarefaUsuario.Usuario?.Foto == null
                            ? tarefaUsuario.Usuario?.Genero == EGenero.Masculino
                                ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                                : _configuration.GetSection("ImageDefaultUser:Feminino").Value
                            : tarefaUsuario.Usuario.Foto,                        
                        Nome = tarefaUsuario.Usuario?.Nome
                    });
                }
            }
            
            retorno.ResponsavelTarefa.Add(new ResponsavelTarefa());
        }

        return retorno;
    }

    public CronogramaAtividadeResponse ConsultarAtividadeCronogramaPorProjeto(int idProjeto)
    {
        var projeto = _service.GetById(idProjeto);
        
        if (projeto == null)
            throw new Exception("Não foi encontrado nenhum registro de projeto!");

        return new CronogramaAtividadeResponse()
        {
            DataFim = projeto.DataFim.ToString("yyyy-M-d"),
            DataInicio = projeto.DataInicio.ToString("yyyy-M-d"),
            DescricaoProjeto = projeto.Titulo,
            LAtividadeCronograma = AtividadeService.GetByIdProjeto(idProjeto)
                .Select(x => new AtividadeCronogramaResponse()
                {
                  IdAtividade = x.IdAtividade,
                  NomeAtividade = x.Titulo,
                  DataInicio = x.DataInicial.ToString("yyyy-M-d"),
                  DataFim = x.DataFim.ToString("yyyy-M-d "),
                }).ToList()
        };
    }

    public ProjetoDashboardResponse ConsultarDashboard(int idProjeto,int idUsuario)
    {
        var feedback = FeedbackReadRepository.GetAll();
        var tarefasUsuario = TarefaService.GetTarefaUsuarioWithInclude()
            .Where(x => x.IdUsuario == idUsuario && x.Tarefa.Status != EStatusTarefa.Completo
                                                 && x.Tarefa.AtividadeFk.DataInicial <= DateTime.Now).ToList();

        var retorno = new ProjetoDashboardResponse
        {
            LProjetos = _service
                .GetAllQuery()
                .Where(x => x.IdUsuarioCadastro == idUsuario && x.Status != EStatusProjeto.Cancelado && x.Status != EStatusProjeto.Concluido)
                .Select(x => new SelectBase()
                {
                    Description = x.Titulo,
                    Value = x.IdProjeto
                }).ToList(),
            LTarefas = tarefasUsuario.GroupBy(x => x.Tarefa.AtividadeFk.ProjetoFk)
                .Select(x => new TarefaDashboard()
                {
                    Projeto = $"#{x.Key.IdProjeto} {x.Key.Titulo}",
                    LTarefa = x.Select(y => y.Tarefa.Descricao).ToList()
                }).ToList(),
            Feedback = new FeedbackDataDashboard()
            {
                TotalFeedback = feedback.Count(),
                MediaFeedback = !feedback.Any() ? 0 : feedback.Sum(x => x.Rating) / feedback.Count(),
                Estrela1 = feedback.Count(x => x.Rating == 1),
                Estrela2 = feedback.Count(x => x.Rating == 2),
                Estrela3 = feedback.Count(x => x.Rating == 3),
                Estrela4 = feedback.Count(x => x.Rating == 4),
                Estrela5 = feedback.Count(x => x.Rating == 5)
            }
        };


        if (retorno.LProjetos.Any())
        {
            Projeto? projeto = null;
        
            if (idProjeto != 0)
            {
                projeto = _service.GetByIdWithIncludes(idProjeto);
        
                if (projeto == null)
                    throw new Exception("Não foi encontrado nenhum registro de projeto!");
            }
            else
            {
                var projetoAny = _service.GetAllQuery()
                    .FirstOrDefault(x =>
                        x.IdUsuarioCadastro == idUsuario && x.Status != EStatusProjeto.Cancelado &&
                        x.Status != EStatusProjeto.Concluido);

                if (projetoAny != null)
                    projeto = _service.GetByIdWithIncludes(projetoAny.IdProjeto);
            }

            if (projeto != null)
            {
                var tarefa = TarefaService.GetTarefasPorAtividades(projeto.Atividades.Select(x => x.IdAtividade).ToList()).ToList();
                var movimentacaoTarefa = MovimentacaoTarefaReadRepository.GetAll();

                retorno.Projeto = new ProjetoDataDashboard()
                {
                    IdProjeto = projeto.IdProjeto,
                    LAtividade = projeto.Atividades.Select(x => new AtividadeDataDashboard
                    {
                        Atividade = x.Titulo,
                        LAtividadeTarefas = x.Tarefas.Select(y => new AtividadeTarefaDataDashboard
                        {
                            TempoTotalTarefasTotal = (y.AtividadeFk.DataFim - y.AtividadeFk.DataInicial).Hours,
                            LTarefas = x.Tarefas.Select(z => new TarefaIndicadoresDataDashboard
                            {
                                IdTarefa = z.IdTarefa,
                                Tarefa = z.Descricao,
                                TempoTarefaEspera = movimentacaoTarefa?
                                    .Where(q => q.From == EStatusTarefa.Aguardando && q.IdTarefa == z.IdTarefa)
                                    .Sum(q => q.TempoUtilizadoUltimaColuna) / 3600 == 0 
                                    ? Convert.ToInt32((DateTime.Now - x.DataInicial).TotalHours)
                                    : movimentacaoTarefa?
                                        .Where(q => q.From == EStatusTarefa.Aguardando && q.IdTarefa == z.IdTarefa)
                                        .Sum(q => q.TempoUtilizadoUltimaColuna) / 3600,
                                TempoTarefaProgresso = (movimentacaoTarefa?
                                    .Where(q => q.From == EStatusTarefa.Progresso && q.IdTarefa == z.IdTarefa)
                                    .Sum(q => q.TempoUtilizadoUltimaColuna) / 3600 ),
                                TempoTarefaTotal = Convert.ToInt32((x.DataFim - x.DataInicial).TotalHours) 
                            }).ToList()
                        }).ToList(),
                        Indicador = new Indicadores()
                        {
                            TarefasFazer = x.Tarefas.Count(y => y.Status == EStatusTarefa.Aguardando && DateTime.Now.Date <= y.AtividadeFk.DataFim.Date && projeto.DataFim.Date > DateTime.Now.Date),
                            TarefasProgresso = x.Tarefas.Count(y => y.Status == EStatusTarefa.Progresso && DateTime.Now.Date <= y.AtividadeFk.DataFim.Date && projeto.DataFim.Date > DateTime.Now.Date),
                            TarefasCompletas = x.Tarefas.Count(y => y.Status == EStatusTarefa.Completo),
                            TarefasAtrasadas = x.Tarefas.Count(y => y.Status !=  EStatusTarefa.Completo && DateTime.Now.Date > y.AtividadeFk.DataFim.Date || projeto.DataFim.Date < DateTime.Now.Date)
                        },
                    }).ToList(),
                    LTarefaIndicador = new Indicadores()
                    {
                        TarefasFazer = tarefa.Count(y => y.Status == EStatusTarefa.Aguardando && DateTime.Now.Date <= y.AtividadeFk.DataFim.Date && projeto.DataFim.Date > DateTime.Now.Date),
                        TarefasProgresso = tarefa.Count(y => y.Status == EStatusTarefa.Progresso && DateTime.Now.Date <= y.AtividadeFk.DataFim.Date && projeto.DataFim.Date > DateTime.Now.Date),
                        TarefasCompletas = tarefa.Count(x => x.Status == EStatusTarefa.Completo),
                        TarefasAtrasadas = tarefa.Count(y => y.Status !=  EStatusTarefa.Completo && DateTime.Now.Date > y.AtividadeFk.DataFim.Date || projeto.DataFim.Date < DateTime.Now.Date)
                    }
                };

                foreach (var atividade in retorno.Projeto.LAtividade)
                {
                    if (atividade.LAtividadeTarefas != null)
                    {
                        foreach (var atividadeTarefa in atividade.LAtividadeTarefas)
                        {
                            if (atividadeTarefa.LTarefas != null)
                            {
                                //Somar tempo em progresso coluna atual
                                foreach (var tarefaMov in atividadeTarefa.LTarefas)
                                {
                                    var colunaAtual = movimentacaoTarefa
                                        .Where(x => x.IdTarefa == tarefaMov.IdTarefa).ToList().MaxBy(x => x.DataCadastro);

                                    if (colunaAtual != null && colunaAtual.To == EStatusTarefa.Progresso)
                                        tarefaMov.TempoTarefaProgresso += (int)(DateTime.Now - colunaAtual.DataCadastro).TotalHours;
                                }
                                
                                atividadeTarefa.TempoTotalTarefasEspera =
                                    atividadeTarefa.LTarefas.Sum(x => x.TempoTarefaEspera);

                                atividadeTarefa.TempoTotalTarefasProgresso =
                                    atividadeTarefa.LTarefas.Sum(x => x.TempoTarefaProgresso);
                            }
                        }
                    }
                }
            }
        }
        
        
        return retorno;
    }
}