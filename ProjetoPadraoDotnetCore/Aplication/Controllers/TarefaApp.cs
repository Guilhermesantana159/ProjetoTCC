using Aplication.Interfaces;
using Aplication.Models.Request.Tarefa;
using Aplication.Models.Response.Tarefa;
using Aplication.Utils.Helpers;
using Aplication.Utils.Objeto;
using Aplication.Validators.Tarefa;
using AutoMapper;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;

namespace Aplication.Controllers;

public class TarefaApp : ITarefaApp
{
    private readonly ITarefaService _service; 
    private readonly IProjetoService _projetoService;
    private readonly ITarefaValidator _validator;
    protected readonly IUsuarioService UsuarioService;
    protected readonly IAtividadeService AtividadeService;
    protected readonly INotificacaoService NotificaService;
    protected readonly IMapper Mapper;
    private readonly IConfiguration _configuration;

    public TarefaApp(ITarefaService service, INotificacaoService notificaService, IMapper mapper,IUsuarioService usuarioService, IConfiguration configuration, ITarefaValidator validator, IAtividadeService atividadeService, IProjetoService projetoService)
    {
        _service = service;
        Mapper = mapper;
        UsuarioService = usuarioService;
        _configuration = configuration;
        _validator = validator;
        AtividadeService = atividadeService;
        _projetoService = projetoService;
        NotificaService = notificaService;
    }


    public ValidationResult Integrar(TarefaAdmRequest request)
    {
        var validator = _validator.ValidacaoCadastro(request);

        if (request.IdTarefa.HasValue)
        {
            var oldTarefa = _service.GetTarefaByIdWithInclude(request.IdTarefa.Value);

            if (oldTarefa == null)
                throw new Exception("Tarefa não encontrado!");
            
            if (oldTarefa.Descricao != request.Descricao)
            {
                if(oldTarefa.AtividadeFk.Tarefas.Any(x => x.Descricao == request.Descricao))
                    validator.LErrors.Add("Não é possível ter tarefas com nomes iguais em uma atividade!");
            }

            if (oldTarefa.AtividadeFk.Tarefas.Count() == 1 && request.IdAtividade != oldTarefa.IdAtividade)
                validator.LErrors.Add($"Esta tarefa é a última da atividade {oldTarefa.AtividadeFk.Titulo}, para deletar esta tarefa adicione mais tarefas nesta atividade ou delete a atividade na tela projetos!");
        }

        if (!validator.IsValid())
            return validator;

        var tarefa = Mapper.Map<TarefaAdmRequest,Tarefa>(request);
        
        var atividade = AtividadeService.GetByIdWithInclude(request.IdAtividade ?? 0);

        if (atividade == null)
            throw new Exception("IdAtividade não encontrado!");

        var lUsuario = UsuarioService
            .GetAllQuery()
            .Where(x => request.LUsuarioIds.Contains(x.IdUsuario))
            .ToList();

        //Atribuições
        tarefa.TarefaUsuario = lUsuario.Select(x => new TarefaUsuario()
        {
            IdUsuario = x.IdUsuario,
            Tarefa = tarefa
        }).ToList();

        tarefa.TagTarefa = request.LTagsTarefa.Select(x => new TagTarefa()
        {
            Descricao = x,
            Tarefa = tarefa
        }).ToList();

        if (!request.IdTarefa.HasValue)
        {
            _service.Cadastrar(tarefa);

            if (atividade.ProjetoFk.AlteracaoTarefasProjetoNotificar)
            {
                //Notificar participantes da sua participação do projeto
                foreach (var user in lUsuario)
                {
                    if (user.IdUsuario != atividade.ProjetoFk.IdUsuarioCadastro)
                    {
                        var pushMensagem = new Notificacao()
                        {
                            IdUsuario = user.IdUsuario,
                            DataCadastro = DateTime.Now,
                            Lido = ESimNao.Nao,
                            ClassficacaoMensagem = EMensagemNotificacao.ParticipacaoProjeto,
                            Corpo = $"Olá, novas tarefas foram atribuido a você no projeto {atividade.ProjetoFk.Titulo}.Acesse \"Tarefas > Registro\" para visualizar",
                            Titulo = $"Participação no projeto {atividade.ProjetoFk.Titulo}",
                            DataVisualização = null,
                        };
            
                        NotificaService.Cadastrar(pushMensagem);
                    }
                }    
            }
        }
        else
        {
            //Remoção antigo 
            var oldTarefa = _service.GetTarefaByIdWithInclude(request.IdTarefa ?? 0);

            if (oldTarefa == null)
                throw new Exception("Não foi encontrado uma tarefa com este Id!");

            if (oldTarefa.TagTarefa.Any())
                _service.DeletarTagsAntigos(oldTarefa.TagTarefa.ToList());

            if (oldTarefa.TarefaUsuario != null && oldTarefa.TarefaUsuario.Any())
                _service.DeletarTarefaUsuarioAntigos(oldTarefa.TarefaUsuario.ToList());
            
            _service.Editar(tarefa);
        }

        return validator;
    }

    public TarefaAdmResponse ConsultarTarefasPorIdProjeto(int idProjeto)
    {
        var projeto = _projetoService.GetById(idProjeto);

        if (projeto == null)
            throw new Exception("Projeto não encontrado!");

        var lAtividadeIds = AtividadeService.GetByIdProjeto(idProjeto)
            .Select(x => new AtividadeAdmResponse()
            {
                IdAtividade = x.IdAtividade,
                Nome = x.Titulo,
                LTarefas = x.Tarefas.Select(y => y.Descricao).ToList()
            })
            .ToList();

        var tarefas = _service.GetTarefasPorAtividades(lAtividadeIds.Select(x => x.IdAtividade).ToList()).ToList();

        var retorno = new TarefaAdmResponse()
        {
            Indicadores = new Indicadores()
            {
                TarefasFazer = tarefas.Count(x => x.Status == EStatusTarefa.Aguardando && DateTime.Now.Date <= x.AtividadeFk.DataFim.Date && projeto.DataFim.Date > DateTime.Now.Date),
                TarefasProgresso = tarefas.Count(x => x.Status == EStatusTarefa.Progresso && DateTime.Now.Date <= x.AtividadeFk.DataFim.Date  && projeto.DataFim.Date > DateTime.Now.Date),
                TarefasCompletas = tarefas.Count(x => x.Status == EStatusTarefa.Completo),
                TarefasAtrasadas = tarefas.Count(x => x.Status !=  EStatusTarefa.Completo && DateTime.Now.Date > x.AtividadeFk.DataFim.Date || projeto.DataFim.Date < DateTime.Now.Date)
            },
            ListTarefas = new List<TarefaAdmListResponse>(),
            ListAtividade = lAtividadeIds,
            IsView = projeto.Status is EStatusProjeto.Cancelado or EStatusProjeto.Concluido,
        };

        foreach (var item  in tarefas)
        {
            var tarefaList = new TarefaAdmListResponse()
            {
                IdTarefa = item.IdTarefa,
                NomeAtividade = item.AtividadeFk.Titulo,
                NomeTarefa = item.Descricao,
                Prioridade = item.Prioridade.ToString(),
                PrioridadeEnum = item.Prioridade.GetHashCode(),
                Status =  projeto?.Status is EStatusProjeto.Cancelado or EStatusProjeto.Concluido ? item.Status.ToString() : 
                projeto != null && ((item.Status != EStatusTarefa.Completo && DateTime.Now.Date > item.AtividadeFk.DataFim) || (DateTime.Now.Date > projeto.DataFim.Date && item.Status != EStatusTarefa.Completo)) 
                    ? "Atrasado" : item.Status.ToString(),
                StatusEnum = item.Status.GetHashCode(),
                DataInicio = item.AtividadeFk.DataInicial.FormatDateBr(),
                DataFim = item.AtividadeFk.DataFim.FormatDateBr(),
                IdAtividade = item.AtividadeFk.IdAtividade,
                LResponsavelTarefa = new List<ResponsavelTarefa>(),
                LTags = item.TagTarefa.Select(x => x.Descricao).ToList(),
                DescricaoTarefa = item.DescricaoTarefa
            };

            if (item.TarefaUsuario != null)
                tarefaList.LResponsavelTarefa.AddRange(item.TarefaUsuario.Where(x => x.Usuario != null)
                    .Select(x => new ResponsavelTarefa()
                    {
                        IdUsuario = x.IdUsuario,
                        Nome = x.Usuario?.Nome,
                        Foto = x.Usuario?.Foto == null
                            ? x.Usuario?.Genero == EGenero.Masculino
                                ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                                : _configuration.GetSection("ImageDefaultUser:Feminino").Value
                            : x.Usuario.Foto
                    }).ToList());

            retorno.ListTarefas.Add(tarefaList);
        }
        
        return retorno;
    }

    public TarefaRegistroResponse ConsultarTarefasRegistro(int idProjeto,int idUsuario)
    {
         var projeto = _projetoService.GetById(idProjeto);
         var usuario = UsuarioService.GetById(idUsuario);

         if (projeto == null)
            throw new Exception("Projeto não encontrado!");
         
         if (usuario == null)
             throw new Exception("Usuário não encontrado!");

         var retorno = new TarefaRegistroResponse()
         {
             ListTarefas = new List<TarefaAdmListResponse>()
         };

         var lAtividadeIds = AtividadeService
             .GetByIdProjeto(idProjeto)
                .Select(x => x.IdAtividade)
                .ToList();

        var tarefas = _service.GetTarefasPorAtividades(lAtividadeIds).ToList();

        foreach (var item  in tarefas)
        {
            var tarefaList = new TarefaAdmListResponse()
            {
                IdTarefa = item.IdTarefa,
                NomeAtividade = item.AtividadeFk.Titulo,
                NomeTarefa = item.Descricao,
                Prioridade = item.Prioridade.ToString(),
                PrioridadeEnum = item.Prioridade.GetHashCode(),
                Status = item.Status.ToString(),
                DataInicio = item.AtividadeFk.DataInicial.FormatDateBr(),
                DataFim = item.AtividadeFk.DataFim.FormatDateBr(),
                StatusEnum = item.Status.GetHashCode(),
                IdAtividade = item.AtividadeFk.IdAtividade,
                LResponsavelTarefa = new List<ResponsavelTarefa>(),
                LTags = item.TagTarefa.Select(x => x.Descricao).ToList(),
                DescricaoTarefa = item.DescricaoTarefa,
                PermiteInicio = item.AtividadeFk.DataInicial <= DateTime.Now,
                Porcentagem = item.Status == EStatusTarefa.Completo ? 100
                     : item.AtividadeFk.DataInicial > DateTime.Now 
                         ? 0 : Convert.ToInt32((DateTime.Now - item.AtividadeFk.DataInicial).TotalDays / (item.AtividadeFk.DataFim - item.AtividadeFk.DataInicial).TotalDays * 100)
            };

            if (item.TarefaUsuario != null)
                tarefaList.LResponsavelTarefa.AddRange(item.TarefaUsuario.Where(x => x.Usuario != null)
                    .Select(x => new ResponsavelTarefa()
                    {
                        IdUsuario = x.IdUsuario,
                        Nome = x.Usuario?.Nome,
                        Foto = x.Usuario?.Foto == null
                            ? x.Usuario?.Genero == EGenero.Masculino
                                ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                                : _configuration.GetSection("ImageDefaultUser:Feminino").Value
                            : x.Usuario.Foto
                    }).ToList());

            retorno.ListTarefas.Add(tarefaList);
        }
        
        //Controle de visualização
        if (projeto.IdUsuarioCadastro != idUsuario && !usuario.PerfilAdministrador)
            retorno.ListTarefas = retorno.ListTarefas.Where(x => x.LResponsavelTarefa != null && x.LResponsavelTarefa.Any(y => y.IdUsuario == idUsuario)).ToList();
            
        return retorno;
    }

    public ValidationResult DeletarTarefaById(int idTarefa)
    {
        var retorno = new ValidationResult();
        var tarefa = _service.GetTarefaByIdWithInclude(idTarefa);
        var deleteAtividade = false;
        
        if(tarefa == null)
            retorno.LErrors.Add("IdTarefa não encontrado!");
        else
        {
            var projeto = _projetoService.GetByIdWithIncludes(tarefa.AtividadeFk.IdProjeto);

            if (tarefa.AtividadeFk.Tarefas.Count() == 1)
            {
                deleteAtividade = true;
                if (projeto != null)
                {
                    if (projeto.Atividades.Count() == 1)
                        retorno.LErrors.Add("Não é possível deletar esta tarefa poís ela é a última de sua atividade,em um projeto é necessário pelo menos um atividade com uma tarefa!");
                }
    
            }

            if (retorno.IsValid())
            {
                _service.DeletarTarefaWithIncludes(tarefa);
                
                if(deleteAtividade)
                    AtividadeService.DeleteById(tarefa.AtividadeFk);
            }
            
            if (projeto != null && projeto.AlteracaoTarefasProjetoNotificar)
            {
                var lUsuarioNotificados = new List<Usuario>();
            
                foreach (var item in tarefa.TarefaUsuario)
                {
                    if (!lUsuarioNotificados.Contains(item.Usuario))
                    {
                        lUsuarioNotificados.Add(item.Usuario);
                    }
                }
            
                //Notificar participantes da sua participação do projeto
                foreach (var user in lUsuarioNotificados)
                {
                    var pushMensagem = new Notificacao()
                    {
                        IdUsuario = user.IdUsuario,
                        DataCadastro = DateTime.Now,
                        Lido = ESimNao.Nao,
                        ClassficacaoMensagem = EMensagemNotificacao.TarefaExcluida,
                        Corpo = $"A tarefa {tarefa.Descricao} ,que foi atribuída a você, foi removida do projeto {projeto?.Titulo}!",
                        Titulo = $"A tarefa {tarefa.Descricao} foi removida!",
                        DataVisualização = null,
                    };
        
                    NotificaService.Cadastrar(pushMensagem);
                }    
            }
        }

        return retorno;
    }

    public ValidationResult IntegrarComentarioTarefa(ComentarioTarefaRequest request)
    {
        var validation = _validator.ValidacaoCadastroComentario(request);

        if (validation.IsValid() && request.IdTarefa.HasValue && request.IdUsuario.HasValue)
        {
            var tarefa = _service.GetTarefaById(request.IdTarefa.Value);
            var usuario = UsuarioService.GetById(request.IdUsuario.Value);
            if (tarefa == null)
                validation.LErrors.Add("Tarefa não encontrada!");
            else if(usuario == null)
                validation.LErrors.Add("Usuário não encontrado!");
            else
            {
              _service.IntegrarComentarioTarefa(Mapper.Map<ComentarioTarefaRequest,ComentarioTarefa>(request));
            }
        }

        return validation;
    }

    public ValidationResult DeletarComentarioById(int? idComentario)
    {
        var retorno = new ValidationResult();

        if (!idComentario.HasValue)
            retorno.LErrors.Add("Id Comentário não encontrado!");
            
        if(retorno.IsValid() && idComentario.HasValue)
            _service.DeletarComentarioTarefa(idComentario.Value);

        return retorno;
    }

    public TarefaDetalhesResponse ConsultarDetalhesTarefa(int idTarefa)
    {
        var entity = _service.GetTarefaByIdWithInclude(idTarefa);

        if (entity == null)
            throw new Exception("Tarefa não encontrada!");

        var projeto = _projetoService.GetById(entity.AtividadeFk.IdProjeto);
        var colunaAtual = entity.MovimentacaoTarefa.MaxBy(x => x.DataCadastro);
        var segundosUtilizados = entity.MovimentacaoTarefa.Where(x => x.From == EStatusTarefa.Progresso)
            .Select(x => x.TempoUtilizadoUltimaColuna).Sum();

        if (colunaAtual != null && colunaAtual.To == EStatusTarefa.Progresso)
            segundosUtilizados += (int)(DateTime.Now - colunaAtual.DataCadastro).TotalSeconds;

        return new TarefaDetalhesResponse()
        {
            TempoUtilizado = $"{(segundosUtilizados / 3600).ToString().PadLeft(2,'0')}:" +
                             $"{((segundosUtilizados % 3600) / 60).ToString().PadLeft(2,'0')}" +
                             $":{(segundosUtilizados % 60).ToString().PadLeft(2,'0')}",
            CodTarefa = entity.IdTarefa.ToString(),
            DataFim = entity.AtividadeFk.DataFim.ToLongDateString(),
            DescricaoTarefa = entity.DescricaoTarefa,
            LComentarios = entity.ComentarioTarefa.Select(x => new ComentarioTarefaResponse()
            {
                Comentario = x.Descricao,
                Foto = x.Usuario.Foto ?? (x.Usuario.Genero == EGenero.Masculino
                    ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                    : _configuration.GetSection("ImageDefaultUser:Feminino").Value),
                Horario = x.Data.ToLongDateString() + " " + x.Data.ToLongTimeString(),
                NomeUsuario = x.Usuario.Nome,
                
            }).ToList(),
            LTags = entity.TagTarefa.Select(x => x.Descricao).ToList(),
            NomeAtividade = entity.AtividadeFk.Titulo,
            NomeProjeto = projeto?.Titulo,
            IsView = projeto?.Status == EStatusProjeto.Cancelado || projeto?.Status == EStatusProjeto.Concluido,
            Titulo = entity.Descricao,
            Status = projeto != null && ((entity.Status != EStatusTarefa.Completo && DateTime.Now.Date > entity.AtividadeFk.DataFim.Date) || DateTime.Now.Date > projeto.DataFim.Date) ? "Atrasado" : entity.Status.ToString(),
            Prioridade = entity.Prioridade.ToString(),
            ResponsavelTarefa = (entity.TarefaUsuario).Where(x => x.Usuario != null)
                .Select(x => new ResponsavelTarefa()
                {
                    IdUsuario = x.IdUsuario,
                    Nome = x.Usuario?.Nome,
                    Foto = x.Usuario?.Foto ?? (x.Usuario?.Genero == EGenero.Masculino
                        ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                        : _configuration.GetSection("ImageDefaultUser:Feminino").Value)
                }).ToList(),
            LMovimentacoes = entity.MovimentacaoTarefa.Select(x => new MovimentacoesResponse()
            {
                Foto = x.Usuario.Foto ?? (x.Usuario.Genero == EGenero.Masculino
                    ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                    : _configuration.GetSection("ImageDefaultUser:Feminino").Value),
                NomeUsuario = x.Usuario.Nome,
                DataMovimentacao = x.DataCadastro.ToString("F"),
                DataMovimentacaoFormatDate = x.DataCadastro.FormatDateBr(),
                De = x.From.ToString(),
                Para = x.To.ToString(),
                TarefaNome = entity.Descricao,
                TempoColuna = $"{(x.TempoUtilizadoUltimaColuna / 3600).ToString().PadLeft(2,'0')}:" +
                              $"{((x.TempoUtilizadoUltimaColuna % 3600) / 60).ToString().PadLeft(2,'0')}" +
                              $":{(x.TempoUtilizadoUltimaColuna % 60).ToString().PadLeft(2,'0')}"
            }).ToList()
        };
    }

    public ValidationResult IntegrarMovimentacao(MovimentacaoTarefaRequest request)
    {
        var retorno = new ValidationResult();
        var usuario = UsuarioService.GetById(request.IdUsuarioMovimentacao);
        var tarefa = _service.GetTarefaById(request.IdTarefa);

        if(usuario == null)
            retorno.LErrors.Add("Usuário não encontrado!");

        if (tarefa == null)
            throw new Exception("Tarefa não encontrado!");
        
        //Antigo status
        request.From = tarefa.Status;

        var atividade = AtividadeService.GetByIdWithInclude(tarefa.IdAtividade);

        if (atividade == null)
            throw new Exception("Atividade desta tarefa não encontrada!");


        if (retorno.IsValid())
        {
            var entity = Mapper.Map<MovimentacaoTarefaRequest,MovimentacaoTarefa>(request);
            var lHistoricoMovimentação = _service.GetMovimentacaoTarefaPorUsuarioTarefa(null,request.IdTarefa);
            
            //Atualização status tarefa
            tarefa.Status = entity.To;
           _service.EditarComRetorno(tarefa);
            
            if (lHistoricoMovimentação != null)
            {
                if (lHistoricoMovimentação.Any())
                {
                    var ultimo = lHistoricoMovimentação.OrderBy(x => x.DataCadastro).FirstOrDefault();

                    if (ultimo != null)
                        entity.TempoUtilizadoUltimaColuna = (long) (DateTime.Now - ultimo.DataCadastro).TotalSeconds;
                }
                else
                    entity.TempoUtilizadoUltimaColuna = (long) (DateTime.Now - atividade.DataInicial).TotalSeconds;
            }
            
            _service.IntegrarMovimentacaoTarefa(entity);
            
            var pushMensagem = new Notificacao()
            {
                IdUsuario = atividade.ProjetoFk.IdUsuarioCadastro,
                DataCadastro = DateTime.Now,
                Lido = ESimNao.Nao,
                ClassficacaoMensagem = EMensagemNotificacao.MovimentacaoTarefa,
                Corpo = $"Olá, a tarefa {tarefa.Descricao} do projeto {atividade.ProjetoFk.Titulo} foi " +
                        $"movimentada de {entity.From.ToString()} para {entity.To.ToString()}",
                Titulo = $"Movimentação da tarefa {tarefa.Descricao} do projeto {atividade.ProjetoFk.Titulo}",
                DataVisualização = null,
            };
            
            NotificaService.Cadastrar(pushMensagem);
        }

        return retorno;
    }

    public MovimentacaoResponse ConsultarMovimentacao(int idAtividade)
    {
        var atividade = AtividadeService.GetByIdWithInclude(idAtividade);
        
        if (atividade == null)
            throw new Exception("Atividade não encontrada!");

        var projeto = _projetoService.GetById(atividade.IdProjeto);

        var retorno = new MovimentacaoResponse
        {
            DataInicioProjeto = projeto?.DataInicio.FormatLongDateBr(),
            DataFimProjeto = projeto?.DataInicio.FormatLongDateBr(),
            NomeProjeto = projeto?.Titulo,
            DataInicioAtividade = atividade.DataInicial.FormatLongDateBr(),
            DataFimAtividade = atividade.DataFim.FormatLongDateBr(),
            NomeAtividade = atividade.Titulo,
            LMovimentacao = new List<MovimentacoesResponse>()
        };

        foreach (var item in atividade.Tarefas)
        {
            retorno.LMovimentacao.AddRange(item.MovimentacaoTarefa.Select(x => new MovimentacoesResponse()
            {
                Foto = x.Usuario.Foto ?? (x.Usuario.Genero == EGenero.Masculino
                    ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                    : _configuration.GetSection("ImageDefaultUser:Feminino").Value),
                NomeUsuario = x.Usuario.Nome,
                DataMovimentacao = x.DataCadastro.ToString("F"),
                DataMovimentacaoFormatDate = x.DataCadastro.FormatLongDateBr(),
                De = x.From.ToString(),
                TarefaNome = item.Descricao,
                Para = x.To.ToString(),
                TempoColuna = $"{(x.TempoUtilizadoUltimaColuna / 3600).ToString().PadLeft(2,'0')}:" +
                              $"{(x.TempoUtilizadoUltimaColuna % 3600 / 60).ToString().PadLeft(2,'0')}" +
                              $":{(x.TempoUtilizadoUltimaColuna % 60).ToString().PadLeft(2,'0')}"
            }).ToList());
        }

        return retorno;
    }
}