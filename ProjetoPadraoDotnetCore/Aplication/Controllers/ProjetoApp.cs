using System.Globalization;
using System.Linq.Dynamic.Core;
using Aplication.Interfaces;
using Aplication.Models.Grid;
using Aplication.Models.Request.Projeto;
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
    private readonly IConfiguration _configuration;

    public ProjetoApp(IProjetoService service, INotificacaoService notificaService, IMapper mapper, IProjetoValidator validation, IUsuarioService usuarioService, ITarefaService tarefaService, IAtividadeService atividadeService, IConfiguration configuration, ITarefaApp tarefaApp)
    {
        _service = service;
        Mapper = mapper;
        Validation = validation;
        UsuarioService = usuarioService;
        TarefaService = tarefaService;
        AtividadeService = atividadeService;
        _configuration = configuration;
        TarefaApp = tarefaApp;
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

    public ValidationResult Editar(ProjetoRequest request)
    {
        var validation = Validation.ValidacaoCadastro(request);
        var lUsuario = UsuarioService.GetAllQuery();
        var projetoOld = _service.GetByIdWithIncludes(request.IdProjeto ?? 0);
        var listIdsUsuarioNotificados = new List<int>();

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
                }
            }
            
            var cadastro = _service.EditarComRetorno(projeto);

            //Atribuição tarefa por usuário
            if (request.Atividade != null)
            {
                foreach (var atv in request.Atividade)
                {
                    var atividadeEntity = Mapper.Map<AtividadeRequest, Atividade>(atv);

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
                                    Descricao = tarefa.Descricao,
                                    IdAtividade = atividade.IdAtividade,
                                    TarefaUsuario = listaUsuarioTarefa
                                });
                            }
                            else
                            {
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
                
                
                if (listIdsUsuarioNotificados.Any())
                {
                    //Notificar participantes da sua participação do projeto
                    foreach (var usuarioNotificacao in listIdsUsuarioNotificados)
                    {
                        if (usuarioNotificacao != projeto.IdUsuarioCadastro)
                        {
                            var pushMensagem = new Notificacao()
                            {
                                IdUsuario = usuarioNotificacao,
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
                itens = itens.Where(x => x.IdUsuarioCadastro == usuarioConsulta.IdUsuario);
            else
            {
                if (request.OnlyAdmin)
                    itens = itens.Where(x => x.IdUsuarioCadastro == usuarioConsulta.IdUsuario);
                else
                {
                    itens = itens
                        .Where(x => x.IdUsuarioCadastro == usuarioConsulta.IdUsuario || x.Atividades
                            .Any(y => y.Tarefas
                                .Any(z => z.TarefaUsuario
                                    .Any(v => v.IdUsuario == request.IdUsuario))));
                }
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
                    FotoProjeto = x.Foto == null 
                            ? _configuration.GetSection("ImageDefaultProjeto:Imagem").Value 
                            : x.Foto,
                    Porcentagem = (x.Status == EStatusProjeto.Cancelado || x.Status == EStatusProjeto.Concluido) ? "100"
                        : x.DataInicio > DateTime.Now 
                            ? "0" : Convert.ToInt64((DateTime.Now - x.DataInicio).TotalDays / (x.DataFim - x.DataInicio).TotalDays * 100).ToString(CultureInfo.InvariantCulture),
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
            else if (DateTime.Now > item.DataFim && item.StatusAtividade != EStatusAtividade.Atrasado && item.Tarefas.All(x => x.Status != EStatusTarefa.Completo))
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
                    IdTarefa = x.IdTarefa
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
            .Where(x => lIdsAtividade.Contains(x.IdAtividade))
            .Select(x => x.IdTarefa);

        var lUsuarioTarefa = TarefaService
            .GetTarefaUsuario()
            .Where(x => lTarefas.Contains(x.IdTarefa ?? 0))
            .ToList()
            .GroupBy(x => x.Usuario);

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
            
        if (atividade?.Tarefas != null)
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
}